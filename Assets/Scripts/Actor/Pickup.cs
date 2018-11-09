using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : Actor
{
    public AudioClip pickupClip;		    // Sound to play when picked up.

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
            Character character = other.gameObject.GetComponent<Character>();
            if (character.isAlive && OnTryPickUp(character))
            {
                if (pickupClip)
                    AudioSource.PlayClipAtPoint(pickupClip, transform.position);

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

    protected abstract bool OnTryPickUp(Character character);
}
