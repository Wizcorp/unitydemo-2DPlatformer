using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameStats
{
    private string nameSettingFile = "gameStats.json";

    public List<LevelStats> levelStats;

    public void SaveLevelStats(LevelStats data)
    {
        if (data == null)
            return;

        if (this.levelStats == null)
            LoadStats();

        int index = this.levelStats.FindIndex(x => x.nameLevel == data.nameLevel);
        if (index == -1)
        {
            this.levelStats.Add(data);
        }
        else
        {
            this.levelStats[index] = data;
        }
        SaveStats();
    }

    public LevelStats LoadLevelStats(string nameLevel)
    {
        LevelStats data = new LevelStats();

        LoadStats();

        int index = this.levelStats.FindIndex(x => x.nameLevel == nameLevel);
        //Initialize if the levelStats don't exist
        if (index == -1)
        {
            data.nameLevel = nameLevel;
            data.higthScore = 0;
            SaveLevelStats(data);
        }
        else
        {
            data = this.levelStats[index];
        }
        return data;
    }

    public void SaveStats()
    {
        string jsonData = JsonHelper.ToJson(this.levelStats, true);
        File.WriteAllText(Application.persistentDataPath + "/" + nameSettingFile, jsonData);
    }

    public void LoadStats()
    {
        if (File.Exists(Application.persistentDataPath + "/" + nameSettingFile))
        {
            string data = File.ReadAllText(Application.persistentDataPath + "/" + nameSettingFile);
            List<LevelStats> dataLevelStats = JsonHelper.FromJson<LevelStats>(data) != null ? JsonHelper.FromJson<LevelStats>(data).ToList() : new List<LevelStats>();
            this.levelStats = dataLevelStats;
        }
        else
        {
            this.levelStats = new List<LevelStats>();
        }
    }
}
