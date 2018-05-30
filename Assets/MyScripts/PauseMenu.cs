using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
	
	// Update is called once per frame
	void Update () {
        //if press esc open pause menu
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused)
            {
                Resume();
            }
            else{
                Pause();
            }
        }
	}

    public void pauseButton(){
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    //function resume the game
    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    //function to pause the game
    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    //function to go back to Main Menu
    public void mainMenu(){
        GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
