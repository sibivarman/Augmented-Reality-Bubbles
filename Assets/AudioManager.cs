using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private AudioSource audioSource;
    public GameObject[] soundSourceObjects;
    public GameObject mainManagerObject;
    public MainManager mainManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        mainManagerObject = GameObject.Find("Game Manager");
        mainManager = mainManagerObject.GetComponent<MainManager>();
        Debug.Log("main Manager:"+mainManager);
    }

    public void setVolume(float vol)
    {
        audioSource.volume = vol;
    }

    public void setSoundVolume(float vol)
    {
        soundSourceObjects = GameObject.FindGameObjectsWithTag("Sound");
        foreach(GameObject thisObject in soundSourceObjects)
        {
            audioSource = thisObject.GetComponentInChildren<AudioSource>();
            audioSource.volume = vol;
        }
        mainManager.setSoundVolume(vol);
    }

    public void setMusicVolume(float vol)
    {
        soundSourceObjects = GameObject.FindGameObjectsWithTag("Music");
        soundSourceObjects[0].GetComponent<AudioSource>().volume = vol;
        mainManager.setMusicVolume(vol);
    }
}
