using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public int LEVEL;

    AudioSource audioManager;

    [SerializeField]
    AudioClip audioclip;
    [SerializeField]
    Sprite StarEnabled;
    [SerializeField]
    Sprite StarDisabled;
    [SerializeField]
    Sprite ButtonEnabled;
    [SerializeField]
    Sprite ButtonDisabled;

    private int UnlockedLvl;

    private GameObject GameManager;
    private int LEVEL_STRING_FIXED_LENGTH = 7;
    private bool IsUnlocked = false;
    private MainManager mainManager;
    private Image LvlBtnImage;
    private Animator anim;

    // Use this for initialization
    void Start () {
        GameManager = GameObject.Find("Game Manager");
        audioManager = GameObject.FindObjectOfType<AudioSource>();
        mainManager = GameManager.GetComponent<MainManager>();
        LvlBtnImage = GetComponentInChildren<Image>();
        anim = GetComponentInChildren<Animator>();
        UnlockedLvl = mainManager.getLevel();
        SetButtonImage();
        SetStars();
        SetAnimation();
    }
	
    public void SetBGM()
    {
        audioManager.clip = audioclip;
        audioManager.volume = 0.25f;
        audioManager.Play();
    }

    private void SetAnimation()
    {
        if(isNewLvl(LEVEL))
            anim.SetBool("IsNew", true);
    }

    private void SetButtonImage()
    {
        if (isButtonUnlocked(LEVEL))
        {
            LvlBtnImage.sprite = ButtonEnabled;
        }
        else
        {
            LvlBtnImage.sprite = ButtonDisabled;

        }
    }

    private bool isNewLvl(int level)
    {
        if (level == UnlockedLvl)
            return true;
        return false;
    }

    private bool isButtonUnlocked(int level)
    {
        if (level <= UnlockedLvl)
            return true;
        return false;
    }

    private void SetLevelValue()
    {
        string st = name.Substring(LEVEL_STRING_FIXED_LENGTH);
        Int32.TryParse(st.Remove(st.Length - 1), out LEVEL);
    }

    private void SetStars()
    {
        MainManager manager = GameManager.GetComponent<MainManager>();
        int starCount = manager.getStars(LEVEL);

        foreach(SpriteRenderer thisObject in GetComponentsInChildren<SpriteRenderer>())
        {
            if (starCount > 0)
            {
                thisObject.sprite = StarEnabled;
                starCount--;
            }
            else
            {
                thisObject.sprite = StarDisabled;
            }
        }

    }
}
