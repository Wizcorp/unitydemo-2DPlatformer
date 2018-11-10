using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 10f;

    [HideInInspector]
    public string shooterTag;

    private bool m_ProjectileHit = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == shooterTag || col.tag == "WorldBoundary" || m_ProjectileHit)
            return;

        m_ProjectileHit = true;

        Actor actor = col.gameObject.GetComponent<Actor>();
        if (actor != null)
        {
            if (actor.isDead)
                return;

            ActorEffect effect;
            effect.type = ActorEffect.Type.Damage;
            effect.amount = damage;
            effect.forceVector = Vector2.zero;

            actor.ApplyEffect(effect);
        }

        OnHit(col);

        Destroy(gameObject);
    }

    protected virtual void OnHit(Collider2D col) { }
}
