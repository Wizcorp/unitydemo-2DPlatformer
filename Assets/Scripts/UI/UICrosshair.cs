using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICrosshair : MonoBehaviour
{
    public Transform pivot;
    public Transform target;

    private SpriteRenderer  m_TargetRenderer;
    private Character       m_Character;

    void Awake()
    {
        m_Character = GetComponentInParent<Character>();
        m_TargetRenderer = target.GetComponent<SpriteRenderer>();
    }

	void Update ()
    {
        if (m_Character && (m_TargetRenderer.enabled = m_Character.isAlive))
        {
            Vector2 orientation = m_Character.GetOrientation();
            orientation.x = Mathf.Abs(orientation.x);
            Quaternion q = Quaternion.FromToRotation(Vector2.right, orientation);
            pivot.localRotation = q;
            target.localRotation = Quaternion.Euler(-q.eulerAngles);
        }
	}
}
