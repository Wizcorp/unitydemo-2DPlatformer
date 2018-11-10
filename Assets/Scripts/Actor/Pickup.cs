using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : Actor
{
    public AudioClip pickupClip;

    private Animator    m_Animator;
    private bool        m_Landed = false;
    private bool        m_Picked = false;

    protected override void Awake()
    {
        base.Awake();
        m_Animator = transform.root.GetComponent<Animator>();
    }

    protected override void OnDied()
    {
        base.OnDied();
        Destroy(transform.root.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (m_Picked || isDead)
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
                m_Picked = true;
            }
        }
        // Otherwise if the crate hits the ground...
        else if (other.tag == "ground" && !m_Landed)
        {
            // ... set the Land animator trigger parameter.
            m_Animator.SetTrigger("Land");

            transform.parent = null;
            gameObject.AddComponent<Rigidbody2D>();
            m_Landed = true;
        }
    }

    protected abstract bool OnTryPickUp(Character character);
}
