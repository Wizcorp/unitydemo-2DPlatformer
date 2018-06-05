using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public Slider volumeSlider;

    public Toggle musicToggle;
    public Toggle effectToggle;

    public GameSettings gameSettings;

    void OnEnable()
    {
        gameSettings = new GameSettings();

        LoadSettings();

        volumeSlider.value = gameSettings.volume;
        musicToggle.isOn = gameSettings.isMusicActive;
        effectToggle.isOn = gameSettings.isEffectSoundActive;

        volumeSlider.onValueChanged.AddListener(delegate { OnChangeVolumeSlider(); });
        musicToggle.onValueChanged.AddListener(delegate { OnChangeMusicToggle(); });
        effectToggle.onValueChanged.AddListener(delegate { OnChangeEffectToggle(); });

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
        this.gameSettings.SaveSettings();
    }

    public void LoadSettings()
    {
        this.gameSettings.LoadSettings();
    }
}
