using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Remover : MonoBehaviour
{
	public GameObject splash;
    public string mainMenuName = "MainMenu";

	void OnTriggerEnter2D(Collider2D col)
	{
		// If the player hits the trigger...
		if(col.gameObject.tag == "Player")
		{
			// .. stop the camera tracking the player
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

            // .. stop the Health Bar following the player
            GameObject healthBarObj = GameObject.FindGameObjectWithTag("HealthBar");
            if (healthBarObj != null)
            {
                if (healthBarObj.activeSelf)
                {
                    healthBarObj.SetActive(false);
                }
            }

			// ... instantiate the splash where the player falls in.
			Instantiate(splash, col.transform.position, transform.rotation);
			// ... destroy the player.
			Destroy (col.gameObject);
            // ... reload the level.
            //StartCoroutine("ReloadGame");

            // Go back to the main menu
            StartCoroutine(ReturnToMainMenu());
		}
		else
		{
			// ... instantiate the splash where the enemy falls in.
			Instantiate(splash, col.transform.position, transform.rotation);

			// Destroy the enemy.
			Destroy (col.gameObject);	
		}
	}

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(mainMenuName);
    }
}
