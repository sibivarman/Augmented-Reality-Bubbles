using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save{

    private int GOLD;
    private int LIFE;
    private int LEVELS;
    private int MAGIC_POTION;
    private bool MUSIC;
    private float SOUND_VOLUME;
    private float MUSIC_VOLUME;
    private Dictionary<int, int> LEVEL_STARS;
    private Dictionary<int, int> AR_CREATURES;

    public int GetGold()
    {
        return GOLD;
    }

    public void SetGold(int value)
    {
        if (value < 0)
            GOLD = 0;
        else
            GOLD = value;
    }

    public int GetLife()
    {
        return LIFE;
    }

    public void SetLife(int value)
    {
        if (value < 0)
            LIFE = 0;
        else
            LIFE = value;
    }


    public int GetLevels()
    {
        return LEVELS;
    }

    public void SetLevels(int value)
    {
        if (value < 0)
            LEVELS = 0;
        else
            LEVELS = value;
    }
    
    public Dictionary<int,int> GetLevelStars()
    {
        return LEVEL_STARS;
    }

    public void SetLevelStars(Dictionary<int,int> value)
    {
        if (value != null)
            LEVEL_STARS = value;
        else
            Debug.LogError("LEVEL_STARS is null, unable to store unlocked level star counts");
    }
   
    public float GetSoundVolume()
    {
        return SOUND_VOLUME;
    }

    public void SetSoundVolume(float value)
    {
        if (value < 0.0f)
            SOUND_VOLUME = 0.0f;
        else
            SOUND_VOLUME = value;
    }

    public float GetMusicVolume()
    {
        return SOUND_VOLUME;
    }

    public void SetMusicVolume(float value)
    {
        if (value < 0.0f)
            MUSIC_VOLUME = 0.0f;
        else
            MUSIC_VOLUME = value;
    }

    public bool GetMusicPlayedStatus()
    {
        return MUSIC;
    }

    public void SetMusicPlayedStatus(bool value)
    {
        MUSIC = value;

    }

    public int GetMagicPotionCount()
    {
        return MAGIC_POTION;
    }

    public void SetMagicPotionCount(int value)
    {
        if (value < 0)
            MAGIC_POTION = 0;
        else
            MAGIC_POTION = value;
    }

    public Dictionary<int,int> GetArCreatures()
    {
        return AR_CREATURES;
    }

    public void SetAR_Creatures(Dictionary<int,int> value)
    {
        if (value != null)
            AR_CREATURES = value;
        else
            Debug.LogError("Unable to set null value for unlocked creatures");
    }


}
