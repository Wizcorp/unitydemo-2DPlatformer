using UnityEngine;
using System.Collections;

public class StoryModeEnemy : Enemy
{

    void Awake()
    {
        // Setting up the references.
        ren = transform.Find("body").GetComponent<SpriteRenderer>();
        frontCheck = transform.Find("frontCheck").transform;
    }

    void FixedUpdate()
    {
        // Create an array of all the colliders in front of the enemy.
        Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1);

        // Check each of the colliders.
        foreach (Collider2D c in frontHits)
        {
            // If any of the colliders is an Obstacle...
            if (c.tag == "Obstacle")
            {
                // ... Flip the enemy and stop checking the other colliders.
                Flip();
                break;
            }
        }

        // Set the enemy's velocity to moveSpeed in the x direction.
        GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

        // If the enemy has one hit point left and has a damagedEnemy sprite...
        if (HP == 1 && damagedEnemy != null)
            // ... set the sprite renderer's sprite to be the damagedEnemy sprite.
            ren.sprite = damagedEnemy;

        // If the enemy has zero or fewer hit points and isn't dead yet...
        if (HP <= 0 && !dead)
            // ... call the death function.
            Death();
    }

    private void Death()
    {
        // Find all of the sprite renderers on this object and it's children.
        SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

        // Disable all of them sprite renderers.
        foreach (SpriteRenderer s in otherRenderers)
        {
            s.enabled = false;
        }

        // Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
        ren.enabled = true;
        ren.sprite = deadEnemy;


        // Set dead to true.
        dead = true;

        // Allow the enemy to rotate and spin it by adding a torque.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb.bodyType == RigidbodyType2D.Kinematic)
            rb.bodyType = RigidbodyType2D.Dynamic;
        rb.fixedAngle = false;
        rb.AddTorque(Random.Range(deathSpinMin, deathSpinMax));

        // Find all of the colliders on the gameobject and set them all to be triggers.
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
        }

        // Play a random audioclip from the deathClips array.
        int i = Random.Range(0, deathClips.Length);
        AudioSource.PlayClipAtPoint(deathClips[i], transform.position);

    }
}
