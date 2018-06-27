using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void loadSurvival()
    {
        SceneManager.LoadScene("Survival");
    }

    public void loadStory()
    {
        SceneManager.LoadScene("Story");
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
