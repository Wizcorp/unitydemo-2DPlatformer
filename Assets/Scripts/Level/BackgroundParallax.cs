using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour
{
	public Transform[]  backgrounds;				// Array of all the backgrounds to be parallaxed.
    public Vector2      parallaxFactor;

	private Transform   m_CameraTransform;
    private Vector3     m_LastCameraPosition;

	void Awake ()
	{
        m_CameraTransform = Camera.main.transform;
	}

	void Start ()
	{
        m_LastCameraPosition = m_CameraTransform.position;
	}

	void Update ()
	{
        Vector2 cameraDisplacement = m_CameraTransform.position - m_LastCameraPosition;

		// For each successive background...
		for(int i = 0; i < backgrounds.Length; i++)
		{
            int distance = backgrounds.Length - i; // layers are aranged back to front

            Vector2 displacement = cameraDisplacement * parallaxFactor * distance;
            backgrounds[i].position += new Vector3(displacement.x, displacement.y, 0f);
		}

        m_LastCameraPosition = m_CameraTransform.position;
	}
}
