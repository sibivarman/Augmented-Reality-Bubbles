using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFiles : MonoBehaviour {

    [SerializeField]
    AudioClip BubbleBurst;
    [SerializeField]
    AudioClip BubbleHit;
    [SerializeField]
    AudioClip BubbleLaunch;

    public AudioClip getBubbleBurst()
    {
        return BubbleBurst;
    }

    public AudioClip getBubbleHit()
    {
        return BubbleHit;
    }

    public AudioClip getBubbleLaunch()
    {
        return BubbleLaunch;
    }
}
