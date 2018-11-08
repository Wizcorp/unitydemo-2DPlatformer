using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 10f;

    [HideInInspector]
    public string shooterTag;

    private bool projectileHit = false;

    protected virtual void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.y) > 20)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == shooterTag || col.tag == "Bullet" || projectileHit)
            return;

        projectileHit = true;

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
