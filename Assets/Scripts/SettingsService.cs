using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class SettingsService
{
    public static GameSettings gameSettings = new GameSettings();

    public static bool isPaused = false;                        // Whether or not is lo paused.

    private static bool isInitializeLoadSettings = false;       // Whether or not the gameSettings is initialize.

    public static void InitializeLoadSettings()
    {
        if (!isInitializeLoadSettings)
        {
            isInitializeLoadSettings = true;
            gameSettings.LoadSettings();
        }
    }

    //Get the effect volume of the current settings
    public static float GetVolumeEffect()
    {
        InitializeLoadSettings();
        //Return gameSettings.volume only if gameSettings.isEffectSoundActive and not paused
        if (gameSettings.isEffectSoundActive && !isPaused)
            return gameSettings.volume;
        else
            return 0;
    }

    //Get the music volume of the current settings
    public static float GetVolumeMusic()
    {
        InitializeLoadSettings();
        //Return gameSettings.volume only if gameSettings.isMusicActive and not paused
        if (gameSettings.isMusicActive && !isPaused)
            return gameSettings.volume;
        else
            return 0;
    }
}

