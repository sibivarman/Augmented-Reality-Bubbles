//-----------------------------------------------------------------------
// <copyright file="HelloARController.cs" company="Google">
//
// Copyright 2017 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.HelloAR
{
    using System.Collections.Generic;
    using GoogleARCore;
    using UnityEngine;

#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = InstantPreviewInput;
#endif

    /// <summary>
    /// Controls the HelloAR example.
    /// </summary>
    public class HelloARController : MonoBehaviour
    {
        /// <summary>
        /// The first-person camera being used to render the passthrough camera image (i.e. AR background).
        /// </summary>
        public Camera FirstPersonCamera;

        /// <summary>
        /// A prefab for tracking and visualizing detected planes.
        /// </summary>
        public GameObject DetectedPlanePrefab;

        /// <summary>
        /// The rotation in degrees need to apply to model when the Andy model is placed.
        /// </summary>
        private const float k_ModelRotation = 180.0f;

        /// <summary>
        /// A list to hold all planes ARCore is tracking in the current frame. This object is used across
        /// the application to avoid per-frame allocations.
        /// </summary>
        private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();

        private List<FeaturePoint> m_AllTrackables = new List<FeaturePoint>();

        /// <summary>
        /// True if the app is in the process of quitting due to an ARCore connection error, otherwise false.
        /// </summary>
        private bool m_IsQuitting = false;

        [SerializeField]
        AudioSource launchBubblel;
        [SerializeField]
        AudioSource bubbleBurst;
        [SerializeField]
        GameObject winScreen;

        public GameObject bubbleSetPrefab;
        public GameObject firingBubblePrefab;

        private bool isPlaced = false;
        private int hitCount = 0;

        private GameObject BubbleSet;

        private List<Vector4> points = new List<Vector4>();

        //

        private int pointCloudThreshold = 5;
        private bool isBubbleSetPlaced = false;
        private bool isGameOver = false;
        private Rotation trajectoryPath;
        private GameObject firingBubble;
        private Sounds bubbleAudios;

        //Google Admob ads
       // private BannerView bannerView;

        public void Start()
        {
            #if UNITY_ANDROID
                string appId = "ca-app-pub-4966957013340912~6721973879";
            #elif UNITY_IPHONE
                string appId = "unexpected_platform";
            #else
                string appId = "unexpected_platform";
            #endif

            // Initialize the Google Mobile Ads SDK.
            //MobileAds.Initialize(appId);
            BubbleSet = Instantiate(bubbleSetPrefab, FirstPersonCamera.transform);
            trajectoryPath = Camera.main.GetComponentInChildren<Rotation>();
            bubbleAudios = GameObject.FindObjectOfType<Sounds>();
        }

        private void Update()
        {
            _UpdateApplicationLifecycle();

            MoveBubbleSetWithCamera();

            if (Input.touchCount < 1 || Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary || isGameOver)
            {
                return;
            }
            Touch touch = Input.GetTouch(0);
            if (!isBubbleSetPlaced)
            {
                PlaceBubbleSet(touch);
            }
            else
            {
                ControlFiringBubble(touch);
            }
            if (BubbleSet.transform.childCount <= 10)
            {
                isGameOver = true;
                winScreen.SetActive(true);
            }

        }

        /*private void RequestBanner()
        {
            #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-4966957013340912/7550881953";
#elif UNITY_IPHONE
                string adUnitId = "unexpected_platform";
#else
                string adUnitId = "unexpected_platform";
#endif

            bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

            AdRequest adRequest = new AdRequest.Builder().Build();
            bannerView.LoadAd(adRequest);
        }*/

        private void PlaceBubbleSet(Touch touch)
        {
            Anchor anchor = Session.CreateAnchor(Frame.Pose);
            BubbleSet.transform.parent = anchor.transform;
            isBubbleSetPlaced = true;
        }

        private void ControlFiringBubble(Touch touch)
        {
            if(touch.phase == TouchPhase.Began)
            {
                trajectoryPath.gameObject.SetActive(true);
                firingBubble = Instantiate(firingBubblePrefab, trajectoryPath.transform.position,Quaternion.identity,Camera.main.transform);
            }
            else if(touch.phase == TouchPhase.Ended && trajectoryPath.gameObject.activeSelf)
            {
                AudioSource.PlayClipAtPoint(bubbleAudios.SoundClips[1], trajectoryPath.transform.position, 0.4f);
                var body = firingBubble.GetComponentsInChildren<Rigidbody>()[1];
                firingBubble.transform.parent = null;
                //Debug.Log(trajectoryPath.transform.up);
                //firingBubble.GetComponentInChildren<BubbleLauncher>().LaunchBubble(trajectoryPath.transform.up);
                //firingBubble.GetComponentInChildren<BubbleLauncher>().LaunchBubble(trajectoryPath.transform.up);
                //body.transform.Translate(trajectoryPath.transform.up*0.5f);
                //body.velocity = trajectoryPath.transform.up;
                //body.solverIterations = 10;
                body.AddForce(trajectoryPath.transform.up*100);
            }
        }

        /// <summary>
        /// The Unity Update() method.
        /// </summary>
       /* public void Update()
        {
            _UpdateApplicationLifecycle();
            _UpdateBubbleSet();
          
            // If the player has not touched the screen, we are done with this update.
            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }
            // Raycast against the location the player touched to search for planes.
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal | TrackableHitFlags.FeaturePoint;

            if (true || Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
            {
                GameObject bubblePrefab;
                if(hitCount == 0)
                {
                    bubblePrefab = BubbleSet;
                    hitCount++;
                    isPlaced = true;
                    var anchor = Session.CreateAnchor(Frame.Pose);
                    //var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                    bubblePrefab.transform.parent = anchor.transform;
                }
                else
                {

                    bubblePrefab = Instantiate(firingBubblePrefab,FirstPersonCamera.transform);
                }   
            }
        }*/

        private void MoveBubbleSetWithCamera()
        {
            if (!isBubbleSetPlaced)
            {
                var transform = BubbleSet.transform;
                transform.position = FirstPersonCamera.transform.forward;
                transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);
            }
        }

        /// <summary>
        /// Check and update the application lifecycle.
        /// </summary>
        private void _UpdateApplicationLifecycle()
        {

            if (Frame.PointCloud.PointCount < pointCloudThreshold)
            {
                return;
            }

            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            // Only allow the screen to sleep when not tracking.
            if (Session.Status != SessionStatus.Tracking)
            {
                const int lostTrackingSleepTimeout = 15;
                Screen.sleepTimeout = lostTrackingSleepTimeout;
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            if (m_IsQuitting)
            {
                return;
            }

            // Quit if ARCore was unable to connect and give Unity some time for the toast to appear.
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                _ShowAndroidToastMessage("Camera permission is needed to run this application.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
            else if (Session.Status.IsError())
            {
                _ShowAndroidToastMessage("ARCore encountered a problem connecting.  Please start the app again.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
        }

        /// <summary>
        /// Actually quit the application.
        /// </summary>
        private void _DoQuit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Show an Android toast message.
        /// </summary>
        /// <param name="message">Message string to show in the toast.</param>
        private void _ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                        message, 0);
                    toastObject.Call("show");
                }));
            }
        }
    }
}
