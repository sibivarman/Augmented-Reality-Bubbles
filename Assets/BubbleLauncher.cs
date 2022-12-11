using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLauncher : MonoBehaviour {

    private int BUBBLE_SPEED_MULTIPLIER = 2;

    public bool isFired;
    public Vector3 direction;

    private GameObject parentObject;

	void Start () {
        parentObject = gameObject.transform.parent.gameObject;
	}

    private void Update()
    {
        if (isFired)
            parentObject.transform.Translate(direction * Time.deltaTime * BUBBLE_SPEED_MULTIPLIER);
    }

    public void LaunchBubble(Vector3 dir)
    {
        direction = dir;
        isFired = true;
    }

}
