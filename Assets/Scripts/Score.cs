using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public int score = 0;                   // The player's score.

    private PlayerControl playerControl;	// Reference to the player control script.
	private int previousScore = 0;			// The score in the previous frame.

    [HideInInspector]
    public int highScore = 0;               // highScore of level

    void Awake ()
	{
		// Setting up the reference.
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

        highScore = StatsService.GetHighScore(SceneManager.GetActiveScene().name);
    }


	void Update ()
	{
		// Set the score text.
		GetComponent<Text>().text = "Score: " + score;

		// If the score has changed...
		if(previousScore != score)
        {
            // ... play a taunt.
            playerControl.StartCoroutine(playerControl.Taunt());
            if (this.score > this.highScore)
            {
                this.highScore = score;
                StatsService.ChangeHighScore(SceneManager.GetActiveScene().name, score);
            }   
        }
			
		// Set the previous score to this frame's score.
		previousScore = score;
	}

}
