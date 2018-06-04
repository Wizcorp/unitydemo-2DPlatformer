using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //Switch to the play scene
    public void PlayGame( int numLevel )
    {
        SceneManager.LoadScene("Level" + numLevel);
    }

    //Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
