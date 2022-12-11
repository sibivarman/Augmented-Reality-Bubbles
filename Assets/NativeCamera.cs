using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeCamera : MonoBehaviour {

    static WebCamTexture cam;

	// Use this for initialization
	void Start () {
        if (cam == null)
            cam = new WebCamTexture();

        GetComponent<Renderer>().material.mainTexture = cam;

        if (!cam.isPlaying)
            cam.Play();
	}

}
