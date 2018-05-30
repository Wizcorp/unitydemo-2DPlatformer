using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //functions of each button for MainMenu
    public void PlayLevelOne(){
        SceneManager.LoadScene("Level1");
    }

    public void PlayLevelTwo()
    {
        SceneManager.LoadScene("Level2");
    }

    public void QuitGame()
	{
        Debug.Log("Quit!!!");
        Application.Quit();
	}
}
