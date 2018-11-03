using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Actor;

public class Pickup : ActorBase
{
    private Animator anim;                  // Reference to the animator component.
    private bool landed;					// Whether or not the crate has landed.

    protected override void Awake()
    {
        base.Awake();
        anim = transform.root.GetComponent<Animator>();
    }

    protected override void OnDied()
    {
        base.OnDied();
        Destroy(transform.root.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the player enters the trigger zone...
        if (other.tag == "Player")
        {
            OnPickedUp(other.gameObject);

            // Destroy the crate.
            Destroy(transform.root.gameObject);
        }
        // Otherwise if the crate hits the ground...
        else if (other.tag == "ground" && !landed)
        {
            // ... set the Land animator trigger parameter.
            anim.SetTrigger("Land");

            transform.parent = null;
            gameObject.AddComponent<Rigidbody2D>();
            landed = true;
        }
    }

    protected virtual void OnPickedUp(GameObject gameObject)
    {
    }
}
