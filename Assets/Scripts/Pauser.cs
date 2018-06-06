using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour {
    public static bool IsPaused = false;

    public GameObject PauseMenuObject;
    public GameObject MusicObject;

    // Use this for initialization
    void Start()
    {
        PauseMenuObject.SetActive(IsPaused);
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause/Resume"))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        IsPaused = false;
        PauseMenuObject.SetActive(IsPaused);
        Time.timeScale = 1;

        if (MusicObject != null)
            MusicObject.GetComponent<AudioSource>().volume = (float)0.1;
    }

    void Pause()
    {
        IsPaused = true;
        PauseMenuObject.SetActive(IsPaused);
        Time.timeScale = 0;

        if (MusicObject != null)
            MusicObject.GetComponent<AudioSource>().volume = 0;
    }

    //Quit the game
    public void QuitGame()
    {
        StatsService.SaveLevelStats();
        Application.Quit();
    }

    public void ReturnMainMenu()
    {
        StatsService.SaveLevelStats();
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        StatsService.SaveLevelStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}