using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthbar : MonoBehaviour
{
    public Transform    actor;
    public Transform    healthBar;
    public bool         deactivateOnDead = true;

    private Actor           actorComponent;
    private SpriteRenderer  healthBarRenderer;
    private float           lastNormalizedHealth = -1f;

    void Awake()
    {
        if (actor != null)
            actorComponent = actor.GetComponent<Actor>();
        else
            actorComponent = GetComponentInParent<Actor>();

        healthBarRenderer = healthBar.GetComponent<SpriteRenderer>();
    }
	
	void Update ()
    {
		if (actorComponent)
        {
            if (actorComponent.isAlive)
            {
                SetHealthBar(actorComponent.health / actorComponent.totalHealth);
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
        if (lastNormalizedHealth == normalizedHealth)
            return;

        lastNormalizedHealth = normalizedHealth;

        Color barColor = Color.Lerp(Color.green, Color.red, 1 - normalizedHealth);
        healthBarRenderer.material.color = barColor;

        healthBar.localScale = new Vector3(normalizedHealth, 1, 1);
    }
}
