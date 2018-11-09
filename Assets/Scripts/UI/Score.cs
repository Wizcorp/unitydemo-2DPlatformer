using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public int score = 0;					// The player's score.


	private PlayerCharacter playerCharacter;	// Reference to the player control script.
	private int previousScore = 0;			    // The score in the previous frame.


	void Awake ()
	{
        // Setting up the reference.
        playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
	}


	void Update ()
	{
		// Set the score text.
		GetComponent<GUIText>().text = "Score: " + score;

		// If the score has changed...
		if(previousScore != score && playerCharacter != null)
            // ... play a taunt.
            playerCharacter.StartCoroutine(playerCharacter.Taunt());

		// Set the previous score to this frame's score.
		previousScore = score;
	}

}
