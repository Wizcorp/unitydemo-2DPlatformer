using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPause : MonoBehaviour {
    
	//Check if the game is currently pause
    //If it is not close everything related to the pause menu
    public GameObject pauseMenuUI;
	// Update is called once per frame
	void Update () {
        if(PauseMenu.GameIsPaused == false){
            pauseMenuUI.SetActive(false);
        }

	}
}
