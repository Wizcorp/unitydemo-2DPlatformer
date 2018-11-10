using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour
{
	private bool m_Paused = false;
	
	void Update ()
    {
		if(Input.GetKeyUp(KeyCode.P))
            m_Paused = !m_Paused;

		if(m_Paused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}
}
