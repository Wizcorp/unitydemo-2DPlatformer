using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour {

    // Script to controll win condition when all enemies are defeated. 
    
    public float enemyCount;

    public GameObject victoryText;

    public void RemoveEnemy()
    {
        enemyCount--;
        if (enemyCount == 0)
        {
            StartCoroutine("Victory");
        }
    }

    IEnumerator Victory()
    {
        // Display victory text
        victoryText.SetActive(true);
        // ... wait briefly
        yield return new WaitForSeconds(2);
        // ... and then return to main menu. 
        SceneManager.LoadScene("Menu");
    }


}
