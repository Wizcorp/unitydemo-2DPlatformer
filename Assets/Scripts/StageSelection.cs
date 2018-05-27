using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelection : MonoBehaviour {

    private static string difficulty = "";

    public static string Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }

    }

    //Display Stage Difficulty
    void OnGUI()
    {
        const int width = 120, height = 60;

        Rect buttonEasy = new Rect(Screen.width / 2 - (width / 2), (2 * Screen.height / 6) - (height / 2), width, height);
        Rect buttonMedium = new Rect(Screen.width / 2 - (width / 2), (2 * Screen.height / 4) - (height / 2), width, height);
        Rect buttonHard = new Rect(Screen.width / 2 - (width / 2), (2 * Screen.height / 3) - (height / 2), width, height);

        if (GUI.Button(buttonEasy, "Easy")) { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Difficulty = "Easy";
        }
        else if (GUI.Button(buttonMedium, "Medium")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Difficulty = "Medium";
        }
        else if (GUI.Button(buttonHard, "Hard")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Difficulty = "Hard";
        }
    }
}
