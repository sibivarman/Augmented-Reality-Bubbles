using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = new Vector3(0,0,-1);
	}

    private void OnTriggerEnter(Collider other)
    {
        FixedJoint springJoint = gameObject.AddComponent<FixedJoint>();
        springJoint.connectedBody = other.GetComponent<Rigidbody>();
    }



}
