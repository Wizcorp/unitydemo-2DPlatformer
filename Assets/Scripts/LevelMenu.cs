using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{

	public Transform[] levelButtons;             // Array of all the levelButton.

	void OnEnable()
	{
		LevelData levelData;

		List<LevelData> levelDataToUnlock;

		int highScore;

		//First, calcul the link between level
		for (int i = 0; i < levelButtons.Length; i++)
		{
			levelData = levelButtons[i].GetComponent<LevelData>();
			if (levelData == null)
				continue;

			highScore = StatsService.GetHighScore(levelData.nameLevel);

			levelDataToUnlock = levelData.buttonLevelToUnlock.ToList().Select(x => x.GetComponent<LevelData>()).ToList();

			if (levelDataToUnlock != null)
			{
				foreach (LevelData dt in levelDataToUnlock)
				{
					if (dt.scoreLock != 0)
					{
						dt.lockedBy = dt.levelTextObject.GetComponent<Text>().text == "" ? levelData.nameLevel : dt.levelTextObject.GetComponent<Text>().text;
						dt.lockedHighScoreBy = highScore;
						dt.locked = true;
					}
					else
					{
						dt.locked = false;
					}
				}
			}
		}

		//Second, verify condition to unlock level
		for (int i = 0; i < levelButtons.Length; i++)
		{
			levelData = levelButtons[i].GetComponent<LevelData>();
			if (levelData == null)
				continue;

			highScore = StatsService.GetHighScore(levelData.nameLevel);

			if (levelData.scoreLock == 0)
			{
				levelData.locked = false;
			}
			else if (levelData.locked)
			{
				if (levelData.lockedHighScoreBy >= levelData.scoreLock)
				{
					levelData.locked = false;
				}
			}

			ChangeStateLock(levelData, levelButtons[i], highScore, levelData.lockedBy);
		}
	}

	//Change the state lock of an button
	private void ChangeStateLock(LevelData levelData, Transform button, int score = 0, string levelToUnlock = "")
	{
		if (levelData.locked)
		{
			levelData.highScoreObject.GetComponent<Text>().text = "Unlock : " + levelData.scoreLock + " in " + levelToUnlock;
			levelData.lockedObject.SetActive(true);
			button.GetComponent<Button>().enabled = false;
		}
		else
		{
			levelData.highScoreObject.GetComponent<Text>().text = "High Score : " + score;
			levelData.lockedObject.SetActive(false);
			button.GetComponent<Button>().enabled = true;
		}
	}


	//Switch to the play scene
	public void PlayGame(string nameLevel)
	{
		for (int i = 0; i < levelButtons.Length; i++)
		{
			LevelData data = levelButtons[i].GetComponent<LevelData>();
			if (data.nameLevel == nameLevel)
			{
				SceneManager.LoadScene(data.nameLevel);
				break;
			}
		}
	}

}
