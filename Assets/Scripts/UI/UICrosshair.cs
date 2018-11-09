using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICrosshair : MonoBehaviour
{
    public Transform pivot;
    public Transform target;

    private SpriteRenderer  targetRenderer;
    private Character       character;

    void Awake()
    {
        character = GetComponentInParent<Character>();
        targetRenderer = target.GetComponent<SpriteRenderer>();
    }

	void Update ()
    {
        if (character && (targetRenderer.enabled = character.isAlive))
        {
            Vector2 orientation = character.GetOrientation();
            orientation.x = Mathf.Abs(orientation.x);
            Quaternion q = Quaternion.FromToRotation(Vector2.right, orientation);
            pivot.localRotation = q;
            target.localRotation = Quaternion.Euler(-q.eulerAngles);
        }
	}
}
