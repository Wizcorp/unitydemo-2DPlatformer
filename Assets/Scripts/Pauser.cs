using UnityEngine;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour {
	private bool paused = false;
    const int width = 120, height = 60;
    Rect buttonRestart = new Rect(Screen.width / 2 - (width / 2), (2 * Screen.height / 4) - (height / 2), width, height);
    Rect buttonMenu = new Rect(Screen.width / 2 - (width / 2), (2 * Screen.height / 3) - (height / 2), width, height);

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyUp(KeyCode.P))
        {
            paused = !paused;
        }

        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
	}

    //Display Pause Menu
    void OnGUI()
    {
        if (paused == true)
        {
            if (GUI.Button(buttonRestart, "Restart"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (GUI.Button(buttonMenu, "Back to Menu"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
            }
        }
    }
}
