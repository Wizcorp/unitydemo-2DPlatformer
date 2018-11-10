using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public int score = 0;					// The player's score.

	private PlayerCharacter m_PlayerCharacter;
	private int             m_PreviousScore = 0;

	void Awake ()
	{
        m_PlayerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
	}

	void Update ()
	{
		// Set the score text.
		GetComponent<GUIText>().text = "Score: " + score;

		// If the score has changed...
		if(m_PreviousScore != score && m_PlayerCharacter)
            // ... play a taunt.
            m_PlayerCharacter.StartCoroutine(m_PlayerCharacter.Taunt());

        // Set the previous score to this frame's score.
        m_PreviousScore = score;
	}

}
