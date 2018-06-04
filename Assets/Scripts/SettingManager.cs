using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public Slider volumeSlider;

    public Toggle musicToggle;
    public Toggle effectToggle;

    private string nameSettingFile = "gameSettings.json";

    public GameSettings gameSettings;

    void OnEnable()
    {
        gameSettings = new GameSettings();

        volumeSlider.onValueChanged.AddListener(delegate { OnChangeVolumeSlider(); });
        musicToggle.onValueChanged.AddListener(delegate { OnChangeMusicToggle(); });
        effectToggle.onValueChanged.AddListener(delegate { OnChangeEffectToggle(); });

        LoadSettings();
    }

    public void OnChangeVolumeSlider()
    {
        gameSettings.volume = volumeSlider.value;
    }

    public void OnChangeMusicToggle()
    {
        gameSettings.isMusicActive = musicToggle.isOn;
    }

    public void OnChangeEffectToggle()
    {
        gameSettings.isEffectSoundActive = effectToggle.isOn;
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/" + nameSettingFile, jsonData);
    }

    public void LoadSettings()
    {
        string data = File.ReadAllText(Application.persistentDataPath + "/" + nameSettingFile);

        if(data != null && data != "")
            gameSettings = JsonUtility.FromJson<GameSettings>(data);

        if (gameSettings != null)
        {
            volumeSlider.value = gameSettings.volume;
            musicToggle.isOn = gameSettings.isMusicActive;
            effectToggle.isOn = gameSettings.isEffectSoundActive;
        }
    }
}
