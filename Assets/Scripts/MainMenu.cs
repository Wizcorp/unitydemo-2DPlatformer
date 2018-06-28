using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void LoadSurvival()
    {
        SceneManager.LoadScene("Survival");
    }

    public void LoadStory()
    {
        SceneManager.LoadScene("Story");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
