using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class StatsService
{
    private static GameStats gameStats = new GameStats();

    //Change the highScore of a nameLevel (not save)
    public static void ChangeHighScore(string nameLevel, int score)
    {
        gameStats.GetLevelStats(nameLevel).higthScore = score;
    }

    //Save the current gameStats
    public static void SaveLevelStats()
    {
        gameStats.SaveStats();
    }

    //Return the highScore of a nameLevel
    public static int GetHighScore(string nameLevel)
    {
        return gameStats.GetLevelStats(nameLevel).higthScore;
    }
}