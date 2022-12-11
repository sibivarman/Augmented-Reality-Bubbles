using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainManager : MonoBehaviour {

    public float soundVolume;
    public float musicVolume;

    [SerializeField]
    SceneLoader sceneLoader;
    [SerializeField]
    AudioManager audioManager;

    private Save SavedGame;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        InitializeSaveGameData();
    }

    public int getStars(int level)
    {
        int star =  SavedGame.GetLevelStars()[level];
        return star;
    }

    public void setSoundVolume(float value)
    {
        SavedGame.SetSoundVolume(value);
    }

    public void setMusicVolume(float value)
    {
        SavedGame.SetMusicVolume(value);
    }

    public float getSoundVolume()
    {
        return SavedGame.GetSoundVolume();
    }

    public float getMusicVolume()
    {
        return SavedGame.GetMusicVolume();
    }

    public int getLevel()
    {
        return SavedGame.GetLevels();
    }

    private void InitializeMusicGameData()
    {
        audioManager.setVolume(SavedGame.GetMusicVolume());
    }

    private void InitializeSaveGameData()
    {
        if (isGameSaved())
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "SaveGame.save", FileMode.Open);
            SavedGame = bf.Deserialize(fs) as Save;
        }
        else
        {
            SavedGame = CreateSaveGame();
            SavedGame.SetGold(0);
            UpdateGameStats();
        }
    }

    private bool isGameSaved()
    {
        if (File.Exists(Application.persistentDataPath + "SaveGame.save")){
            return true;
        }
        return false;
    }

    private Save CreateSaveGame()
    {
        return new Save();
    }

    public void SaveGame()
    {
        UpdateGameStats();
        FileStream file = File.Create(Application.persistentDataPath + "SaveGame.save");
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, SavedGame);
        file.Close();
    }

    private void UpdateGameStats()
    {
        SavedGame.SetGold(0);
        SavedGame.SetLevels(4);
        SavedGame.SetLevelStars(new Dictionary<int, int>());
        SetDummy();
        SavedGame.SetLife(5);
        SavedGame.SetMagicPotionCount(5);
        SavedGame.SetMusicPlayedStatus(true);
        SavedGame.SetMusicVolume(1.0f);
        SavedGame.SetAR_Creatures(new Dictionary<int, int>());
    }

    private void SetDummy()
    {
        SavedGame.GetLevelStars().Add(1, 1);
        SavedGame.GetLevelStars().Add(2, 2);
        SavedGame.GetLevelStars().Add(3, 3);
        SavedGame.GetLevelStars().Add(4, 2);
        SavedGame.GetLevelStars().Add(5, 1);
    }

}
