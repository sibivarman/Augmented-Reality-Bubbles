using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.Examples.Common;

#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class Rotation : MonoBehaviour {


    private float angleRotate = 4f;

    void Update () {

        if(Input.touchCount < 1)
        {
            Reset();
            gameObject.SetActive(false);
            return;
        }
        float rotX = Input.GetTouch(0).deltaPosition.x * Mathf.Deg2Rad * angleRotate;
        float rotY = Input.GetTouch(0).deltaPosition.y * Mathf.Deg2Rad * angleRotate;
        transform.Rotate(Vector3.back, rotX);
        transform.Rotate(Vector3.right,-rotY);
        
    }

    private void Reset()
    {
        transform.localEulerAngles = new Vector3(90, 0, 0);
        //transform.eulerAngles = new Vector3(90, 0, 0);

    }


}
