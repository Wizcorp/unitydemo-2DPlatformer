using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class SettingsService
{
    public static GameSettings gameSettings = new GameSettings();

    //Get the effect volume of the current settings
    public static float GetVolumeEffect()
    {
        //Return gameSettings.volume only if gameSettings.isEffectSoundActive
        if (gameSettings.isEffectSoundActive)
            return gameSettings.volume;
        else
            return 0;
    }

    //Get the music volume of the current settings
    public static float GetVolumeMusic()
    {
        //Return gameSettings.volume only if gameSettings.isMusicActive
        if (gameSettings.isMusicActive)
            return gameSettings.volume;
        else
            return 0;
    }
}

