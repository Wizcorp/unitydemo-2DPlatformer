using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class SoundService
{
    public static GameSettings gameSettings = null;

    public static float GetVolumeEffect()
    {
        if (gameSettings == null)
        {
            gameSettings = new GameSettings();
            gameSettings.LoadSettings();
        }   
        if (gameSettings == null)
            return 0;

        if (gameSettings.isEffectSoundActive)
            return gameSettings.volume;
        else
            return 0;
    }

    public static float GetVolumeMusic()
    {
        if (gameSettings == null)
        {
            gameSettings = new GameSettings();
            gameSettings.LoadSettings();
        }
        if (gameSettings == null)
            return 0;

        if (gameSettings.isMusicActive)
            return gameSettings.volume;
        else
            return 0;
    }
}

