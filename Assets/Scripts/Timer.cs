using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    
    public float TimeLeft;

	void Update () {
        TimeLeft -= Time.deltaTime;

        string minutes = ((int)TimeLeft / 60).ToString();
        string seconds = (TimeLeft % 60).ToString("f2");

        GetComponent<GUIText>().text = "Time: " + minutes + seconds;
        if (TimeLeft <= 10)
        {
            GetComponent<GUIText>().color = Color.red;
            if (TimeLeft <= 0)
            {
                //Game Over
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }        
	}
}
