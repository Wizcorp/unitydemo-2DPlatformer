using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Actor
{
    private Animator anim;                  // Reference to the animator component.
    private bool landed;					// Whether or not the crate has landed.
    private bool picked = false;

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
        if (picked || isDead)
            return;

        // If the player enters the trigger zone...
        if (other.tag == "Player")
        {
            if (other.gameObject.GetComponent<Actor>().isAlive)
            {
                OnPickedUp(other.gameObject);

                // Destroy the crate.
                Destroy(transform.root.gameObject);
                picked = true;
            }
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
