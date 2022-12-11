using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FireBubbleHolder>())
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<SphereHolder>().isAlive = true;
        }
    }
}
