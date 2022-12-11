using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereHolder : MonoBehaviour {

    public bool isAlive = true;

    private bool hasBubble;
    private int attachedBubbleCount = 0;

    private DestroyBubble destroyBubble;

	// Use this for initialization
	void Start () {
		if(gameObject.transform.childCount > 0)
        {
            hasBubble = true;
            destroyBubble = GetComponentInChildren<DestroyBubble>();
        }
        else
        {
            Destroy(gameObject);
        }
	}

    private void Update()
    {
        if (isAlive)
        {
            attachedBubbleCount = Physics.OverlapBox(transform.position, new Vector3(0.6f, 0.6f, 0.6f)).Length / 2;
            if (attachedBubbleCount <= 1)
            {
                isAlive = false;
                destroyBubble.DestroySphere();
            }
        }
    }
}
