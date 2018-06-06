using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameStats
{
    private string nameSettingFile = "gameStats.json";

    public List<LevelStats> levelStats;

    //Get the LevelStats of level by nameLevel
    public LevelStats GetLevelStats(string nameLevel)
    {
        LevelStats data = new LevelStats();

        //Load if the levelStats is null
        if (levelStats == null)
            LoadStats();

        int index = this.levelStats.FindIndex(x => x.nameLevel == nameLevel);
        //Initialize if the levelStats don't exist
        if (index == -1)
        {
            data.nameLevel = nameLevel;
            data.higthScore = 0;
            this.levelStats.Add(data);
            SaveStats();
        }
        else
        {
            data = this.levelStats[index];
        }
        return data;
    }

    //Save the current levelStats list in json file
    public void SaveStats()
    {
        string jsonData = JsonHelper.ToJson(this.levelStats, true);
        File.WriteAllText(Application.persistentDataPath + "/" + nameSettingFile, jsonData);
    }

    //Load the levelStats list by file or levelStats list
    public void LoadStats()
    {
        //Verify if the file exist
        if (File.Exists(Application.persistentDataPath + "/" + nameSettingFile))
        {
            string data = File.ReadAllText(Application.persistentDataPath + "/" + nameSettingFile);
            List<LevelStats> dataLevelStats = JsonHelper.FromJson<LevelStats>(data) != null ? JsonHelper.FromJson<LevelStats>(data).ToList() : new List<LevelStats>();
            this.levelStats = dataLevelStats;
        }
        //Initialize levelStats list if the file don't exist
        else
        {
            this.levelStats = new List<LevelStats>();
        }
    }
}
