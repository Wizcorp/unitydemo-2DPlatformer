
using System.IO;
using UnityEngine;

public class GameSettings
{
    public float volume;

    public bool isMusicActive;
    public bool isEffectSoundActive;

    private string nameSettingFile = "gameSettings.json";

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(this, true);
        File.WriteAllText(Application.persistentDataPath + "/" + nameSettingFile, jsonData);
    }

    public void LoadSettings()
    {
        if (File.Exists(Application.persistentDataPath + "/" + nameSettingFile))
        {
            string data = File.ReadAllText(Application.persistentDataPath + "/" + nameSettingFile);
            GameSettings gameSettings = JsonUtility.FromJson<GameSettings>(data);
            this.volume = gameSettings.volume;
            this.isMusicActive = gameSettings.isMusicActive;
            this.isEffectSoundActive = gameSettings.isEffectSoundActive;
        }
        //Initialize GameSettings if the file don't exist
        else
        {
            this.volume = 1;
            this.isMusicActive = true;
            this.isEffectSoundActive = true;
            this.SaveSettings();
        }
    }
}
