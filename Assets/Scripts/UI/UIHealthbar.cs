using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthbar : MonoBehaviour
{
    public Transform    actor;
    public Transform    healthBar;
    public bool         deactivateOnDead = true;

    private Actor           m_Actor;
    private SpriteRenderer  m_HealthBarRenderer;
    private float           m_LastNormalizedHealth = -1f;

    void Awake()
    {
        if (actor != null)
            m_Actor = actor.GetComponent<Actor>();
        else
            m_Actor = GetComponentInParent<Actor>();

        m_HealthBarRenderer = healthBar.GetComponent<SpriteRenderer>();
    }
	
	void Update ()
    {
		if (m_Actor)
        {
            if (m_Actor.isAlive)
            {
                SetHealthBar(m_Actor.health / m_Actor.totalHealth);
            }
            else if (deactivateOnDead)
            {
                gameObject.SetActive(false);
            }
            else
            {
                SetHealthBar(0f);
            }
        }
	}

    void LateUpdate()
    {
        InverseParentFlipAndRotate();
    }

    void InverseParentFlipAndRotate()
    {
        Quaternion rotation = transform.rotation;
        Vector3 globalScale = transform.lossyScale;
        Vector3 currentScale = transform.localScale;

        transform.rotation = Quaternion.Euler(-rotation.eulerAngles);
        transform.localScale = new Vector3(currentScale.x * Mathf.Sign(globalScale.x), 
            currentScale.y, currentScale.z);
    }

    void SetHealthBar(float normalizedHealth)
    {
        if (m_LastNormalizedHealth == normalizedHealth)
            return;

        m_LastNormalizedHealth = normalizedHealth;

        Color barColor = Color.Lerp(Color.green, Color.red, 1 - normalizedHealth);
        m_HealthBarRenderer.material.color = barColor;

        healthBar.localScale = new Vector3(normalizedHealth, 1, 1);
    }
}
