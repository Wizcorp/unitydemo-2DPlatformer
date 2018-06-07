
using System.IO;
using UnityEngine;

public class GameSettings
{
    public float volume;                // Volume settings of effect and music.

    public bool isMusicActive;          // Whether or not the music is activated.
    public bool isEffectSoundActive;    // Whether or not the effect is activated.

    private string nameSettingFile = "gameSettings.json"; // Name of the settings file.

    //Save the current settings in json file
    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(this, true);
        File.WriteAllText(Application.persistentDataPath + "/" + nameSettingFile, jsonData);
    }

    //Load the settings by file or initialize settings
    public void LoadSettings()
    {
        //Verify if the file exist
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
