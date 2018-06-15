using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Pauser : MonoBehaviour {

    private bool paused = false;

    private bool resume = true;

    public GameObject pauseFilter;

    public GameObject pauseMenu;

    public GameObject selection;

    void Start()
    {
        pauseMenu.SetActive(false);
        pauseFilter.SetActive(false);
        selection.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyUp(KeyCode.P))
		{
			paused = !paused;
		}

        if (paused)
        {
			Time.timeScale = 0;

            pauseMenu.SetActive(true);
            pauseFilter.SetActive(true);
            selection.SetActive(true);

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                resume = !resume;
            }

            if (resume)
            {
                //3
                selection.transform.localPosition = new Vector2(0, 3);
                selection.GetComponent<RawImage>().color = Color.green;
            }
            else if (true)
            {
                //-104
                selection.transform.localPosition = new Vector2(0, -104);
                selection.GetComponent<RawImage>().color = Color.red;
            }

            if (resume && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
            {
                paused = false;
            }
            else if (!resume && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
			Time.timeScale = 1;
            pauseMenu.SetActive(false);
            pauseFilter.SetActive(false);
            selection.SetActive(false);
        }
	}
}
