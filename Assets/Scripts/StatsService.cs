﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class StatsService
{
    private static GameStats gameStats = new GameStats();

    private static bool isInitializeGameStats = false;      // Whether or not the gameSettings is initialize.

    public static void InitializeLoadStats()
    {
        if (!isInitializeGameStats)
        {
            isInitializeGameStats = true;
            gameStats.LoadStats();
        }
    }

    //Change the highScore of a nameLevel (not save)
    public static void ChangeHighScore(string nameLevel, int score)
    {
        InitializeLoadStats();
        gameStats.GetLevelStats(nameLevel).higthScore = score;
    }

    //Save the current gameStats
    public static void SaveLevelStats()
    {
        InitializeLoadStats();
        gameStats.SaveStats();
    }

    //Return the highScore of a nameLevel
    public static int GetHighScore(string nameLevel)
    {
        InitializeLoadStats();
        return gameStats.GetLevelStats(nameLevel).higthScore;
    }
}