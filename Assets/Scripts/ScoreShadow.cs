using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreShadow : MonoBehaviour
{
	public GameObject guiCopy;      // A copy of the score.

	void Awake()
	{
		// Set the position to be slightly down and behind the other gui.
		Vector3 behindPos = transform.position;
		behindPos = new Vector3(guiCopy.transform.position.x, guiCopy.transform.position.y - 3f, guiCopy.transform.position.z);
		transform.position = behindPos;
	}


	void Update()
	{
		// Set the text to equal the copy's text.
		GetComponent<Text>().text = guiCopy.GetComponent<Text>().text;
	}
}
