using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //Display Game Menu
    void OnGUI()
    {
        const int width = 120, height = 60;

        Rect buttonRect = new Rect(Screen.width / 2 - (width / 2), (2 * Screen.height / 3) - (height / 2), width, height);

        if (GUI.Button(buttonRect, "Start New Game"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
