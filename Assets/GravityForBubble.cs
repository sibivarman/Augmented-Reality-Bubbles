using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForBubble : MonoBehaviour {

    [SerializeField]
    int xSize, ySize, zSize;
    [SerializeField]
    float pullSpeed;

    private GameObject bubble;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //foreach (Collider collider in Physics.OverlapBox(transform.position, new Vector3(xSize,ySize,zSize))){
        /* foreach (Collider collider in Physics.OverlapSphere(transform.position, 1))
         {
             Vector3 forceDirection = transform.position - collider.transform.position;
             collider.GetComponent<Rigidbody>().AddForce(forceDirection * pullSpeed * Time.deltaTime);
         }*/

        if (transform.position != transform.GetChild(0).position + new Vector3(0.25f, 0.25f, 0.25f))
        {
            //transform.GetChild(0).GetComponent<Rigidbody>().AddForce(transform.position - transform.GetChild(0).transform.position);
            transform.GetChild(0).transform.position = Vector3.MoveTowards(transform.position,transform.GetChild(0).position,pullSpeed*Time.deltaTime);
        }

	}
}
