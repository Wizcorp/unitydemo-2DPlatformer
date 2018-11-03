using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actor
{
    public class ActorBase : MonoBehaviour
    {
        public float    totalHealth = 100f;
        public bool     indestructable = false;

        [HideInInspector]
        public bool     isDead = false;

        [HideInInspector]
        public float    health;

        protected virtual void Awake()
        {
            if (isDead)
                health = 0f;
            else
                health = totalHealth;
        }

        public virtual void ApplyEffect(ActorEffect actorEffect)
        {
            if (actorEffect.forceVector != Vector2.zero)
            {
                GetComponent<Rigidbody2D>().AddForce(actorEffect.forceVector);
            }

            if (isDead)
                return;

            switch (actorEffect.type)
            {
                case ActorEffect.Type.Damage:
                    OnDamageReceived(actorEffect.amount);
                    if (health == 0f)
                    {
                        isDead = true;
                        OnDied();
                    }
                    break;
                case ActorEffect.Type.Heal:
                    OnHealReceived(actorEffect.amount);
                    break;
                case ActorEffect.Type.Kill:
                    if (!indestructable)
                    {
                        health = 0f;
                        isDead = true;
                        OnDied();
                    }
                    break;
            }
        }

        protected virtual void OnDamageReceived(float amount)
        {
            if (!indestructable)
                health = Mathf.Max(health - amount, 0f);
        }

        protected virtual void OnHealReceived(float amount)
        {
            health = Mathf.Min(health + amount, totalHealth);
        }

        protected virtual void OnDied() { }
    }
}