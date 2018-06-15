using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    Color col;

    public GameObject continueTxt;

    public GameObject titleTxt;

    public GameObject titleScreen;

    public GameObject mainMenu;

    public GameObject levelSelect;

    public GameObject selection;

    private float posY;

    private bool startGame;

    private int index = 0;

    // Use this for initialization
    void Start()
    {
        col = continueTxt.GetComponent<Text>().color;
        col.a = 0.0f;
        continueTxt.GetComponent<Text>().color = col;

        posY = titleTxt.transform.localPosition.y;

        titleScreen.SetActive(true);
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        selection.SetActive(false);

        startGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        continueText();

        title();

        loadMainMenu();

        select();
    }

    void title()
    {
        if (titleTxt.transform.localPosition.y > -31.0f)
        {
            posY -= 5.5f;
        }

        titleTxt.transform.localPosition = new Vector3(titleTxt.transform.localPosition.x, posY, titleTxt.transform.localPosition.z);
    }

    void continueText()
    {
        if (col.a <= 1.0f)
        {
            col.a += 0.008f;
        }

        continueTxt.GetComponent<Text>().color = col;
    }

    void loadMainMenu()
    {
        if (Input.anyKeyDown && !levelSelect.activeSelf)
        {
            titleScreen.SetActive(false);
            mainMenu.SetActive(true);
            levelSelect.SetActive(false);
            selection.SetActive(true);
        }
    }

    void select()
    {
        if (mainMenu.activeSelf)
        {
            mainMenuCode();
        }
        else if (levelSelect.activeSelf)
        {
            levelSelectCode();
        }
    }

    void mainMenuCode()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            startGame = !startGame;
        }

        if (startGame)
        {
            selection.transform.localPosition = new Vector2(selection.transform.localPosition.x, 31.0f);
            selection.GetComponent<RawImage>().color = Color.green;
        }
        else if (!startGame)
        {
            selection.transform.localPosition = new Vector2(selection.transform.localPosition.x, -77.0f);
            selection.GetComponent<RawImage>().color = Color.red;
        }

        if (startGame && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
        {
            mainMenu.SetActive(false);
            levelSelect.SetActive(true);
        }
        else if (!startGame && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
        {
            Application.Quit();
        }
    }

    void levelSelectCode()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            index--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            index++;
        }

        if (index < 0)
        {
            index = 2;
        }
        else if (index > 2)
        {
            index = 0;
        }

        if (index == 0)
        {
            selection.transform.localPosition = new Vector2(selection.transform.localPosition.x, 31.0f);
            selection.GetComponent<RawImage>().color = Color.white;
        }
        else if (index == 1)
        {
            selection.transform.localPosition = new Vector2(selection.transform.localPosition.x, -77.0f);
            selection.GetComponent<RawImage>().color = Color.white;
        }
        else if (index == 2)
        {
            selection.transform.localPosition = new Vector2(selection.transform.localPosition.x, -185.0f);
            selection.GetComponent<RawImage>().color = Color.red;
        }

        if (index == 0 && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
        {
            SceneManager.LoadScene("Level");
        }
        else if (index == 1 && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
        {
            SceneManager.LoadScene("Level 2");
        }
        else if (index == 2 && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
        {
            mainMenu.SetActive(true);
            levelSelect.SetActive(false);
        }
    }
}