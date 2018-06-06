using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Remover : MonoBehaviour
{
	public GameObject splash;
    public GameObject gameOver;

	void OnTriggerEnter2D(Collider2D col)
	{
        splash.GetComponent<AudioSource>().volume = SettingsService.GetVolumeEffect();
        // If the player hits the trigger...
        if (col.gameObject.tag == "Player")
		{
			// .. stop the camera tracking the player
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

			// .. stop the Health Bar following the player
			if(GameObject.FindGameObjectWithTag("HealthBar").activeSelf)
			{
				GameObject.FindGameObjectWithTag("HealthBar").SetActive(false);
			}

			// ... instantiate the splash where the player falls in.
			Instantiate(splash, col.transform.position, transform.rotation);
			// ... destroy the player.
			Destroy (col.gameObject);
			// ... quit the game.
			StartCoroutine("QuitGame");
		}
		else
		{
            // ... instantiate the splash where the enemy falls in.
            Instantiate(splash, col.transform.position, transform.rotation);

			// Destroy the enemy.
			Destroy (col.gameObject);	
		}
	}

	IEnumerator QuitGame()
	{			
        // ... save levelStats
        StatsService.SaveLevelStats();
        // ... setActive gameOver
        gameOver.SetActive(true);
        // ... pause briefly
        yield return new WaitForSeconds(2);
        // ... and then go to the main menu.
        SceneManager.LoadScene("MainMenu");
    }
}
