using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour
{
	public Transform[]  backgrounds;				// Array of all the backgrounds to be parallaxed.
    public Vector2      parallaxFactor;

	private Transform cam;						// Shorter reference to the main camera's transform.
	private Vector3 previousCamPos;				// The postion of the camera in the previous frame.


	void Awake ()
	{
		// Setting up the reference shortcut.
		cam = Camera.main.transform;
	}

	void Start ()
	{
		// The 'previous frame' had the current frame's camera position.
		previousCamPos = cam.position;
	}

	void Update ()
	{
        Vector2 cameraDisplacement = cam.position - previousCamPos;

		// For each successive background...
		for(int i = 0; i < backgrounds.Length; i++)
		{
            int distance = backgrounds.Length - i; // layers are aranged back to front

            Vector2 displacement = cameraDisplacement * parallaxFactor * distance;
            backgrounds[i].position += new Vector3(displacement.x, displacement.y, 0f);
		}

		// Set the previousCamPos to the camera's position at the end of this frame.
		previousCamPos = cam.position;
	}
}
