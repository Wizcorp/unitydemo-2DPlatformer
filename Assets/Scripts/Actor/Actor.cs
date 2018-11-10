using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public float    totalHealth = 100f;
    public bool     indestructable = false;

    private float m_Health = 0f;

    public float health
    {
        get { return m_Health; }
    }

    public bool isAlive
    {
        get { return m_Health >= Mathf.Epsilon; }
    }

    public bool isDead
    {
        get { return m_Health < Mathf.Epsilon; }
    }

    protected virtual void Awake()
    {
        m_Health = totalHealth;
    }

    public virtual void ApplyEffect(ActorEffect actorEffect)
    {
        if (actorEffect.forceVector != Vector2.zero)
        {
            Rigidbody2D body = GetComponent<Rigidbody2D>();
            if (body)
                body.AddForce(actorEffect.forceVector);
        }

        if (isDead)
            return;

        switch (actorEffect.type)
        {
            case ActorEffect.Type.Damage:
                OnDamageReceived(actorEffect.amount);
                if (isDead)
                    OnDied();
                break;
            case ActorEffect.Type.Heal:
                OnHealReceived(actorEffect.amount);
                break;
            case ActorEffect.Type.Kill:
                if (!indestructable)
                {
                    m_Health = 0f;
                    OnDied();
                }
                break;
            }
        }

    protected virtual void OnDamageReceived(float amount)
    {
        if (!indestructable)
            m_Health = Mathf.Max(m_Health - amount, 0f);
    }

    protected virtual void OnHealReceived(float amount)
    {
        m_Health = Mathf.Min(m_Health + amount, totalHealth);
    }

    protected virtual void OnDied() { }
}