using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBubble : MonoBehaviour {

    private DestroyBubble destroyBubble;

    private void Start()
    {
        destroyBubble = GetComponentInChildren<DestroyBubble>();
    }


}
