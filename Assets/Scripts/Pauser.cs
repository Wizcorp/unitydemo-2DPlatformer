using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour
{
	public GameObject PauseMenuObject;

	// Use this for initialization
	void Start()
	{
		Resume();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Pause/Resume"))
		{
			if (SettingsService.isPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	//Desactivate PauseMenu
	public void Resume()
	{
		SettingsService.isPaused = false;
		PauseMenuObject.SetActive(SettingsService.isPaused);
		Time.timeScale = 1;
	}

	//Activate PauseMenu
	void Pause()
	{
		SettingsService.isPaused = true;
		PauseMenuObject.SetActive(SettingsService.isPaused);
		Time.timeScale = 0;
	}


	//Quit the game
	public void QuitGame()
	{
		StatsService.SaveLevelStats();
		Application.Quit();
	}

	public void ReturnMainMenu()
	{
		StatsService.SaveLevelStats();
		SceneManager.LoadScene("MainMenu");
	}

	public void RestartGame()
	{
		StatsService.SaveLevelStats();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}