using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

    //Display Gameover Menu
	void OnGUI(){
		const int width = 120, height = 60;

        Rect buttonRetry = new Rect(Screen.width / 2 - (width / 2), (2 * Screen.height / 4) - (height / 2), width, height);
        Rect buttonMenu = new Rect(Screen.width / 2 - (width / 2), (2 * Screen.height / 3) - (height / 2), width, height);
        
        if (GUI.Button(buttonRetry, "Retry"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        else if(GUI.Button(buttonMenu, "Back to menu"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }
}
