using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    public GameObject score;

    public GameObject againText;

    public GameObject exitText;

    public GameObject selection;

    private bool tryAgain;

    // Use this for initialization
    void Start()
    {
        againText.SetActive(false);
        exitText.SetActive(false);
        selection.SetActive(false);
        Remover.dead = false;
        tryAgain = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Remover.dead)
        {
            if (score.transform.localPosition.y > 0.6f)
            {
                score.transform.localPosition = new Vector2(score.transform.localPosition.x, score.transform.localPosition.y - 2.5f);
            }
            else if (true)
            {
                againText.SetActive(true);
                exitText.SetActive(true);
                selection.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                tryAgain = !tryAgain;
            }



            if (tryAgain)
            {
                selection.transform.localPosition = new Vector2(-189.74f, selection.transform.localPosition.y);
                selection.GetComponent<RawImage>().color = Color.green;
            }
            else if (!tryAgain)
            {
                selection.transform.localPosition = new Vector2(189.74f, selection.transform.localPosition.y);
                selection.GetComponent<RawImage>().color = Color.red;
            }

            if (tryAgain && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Remover.dead = false;
            }
            else if (!tryAgain && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
