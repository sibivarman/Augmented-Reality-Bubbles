using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour {

    [SerializeField]
    public AudioClip[] SoundClips;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetClip(int index)
    {
        audioSource.clip = SoundClips[index];
    }

    public void SetVolume(float vol)
    {
        audioSource.volume = vol;
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void PlayBubbleBurst()
    {
        SetClip(0);
        PlaySound();
    }

    public void PlayBubbleLaunch()
    {
        SetClip(1);
        PlaySound();
    }

    public void PlayBubbleHit()
    {
        SetClip(2);
        PlaySound();
    }

}
