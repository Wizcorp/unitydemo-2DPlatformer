using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
    private Character character;

    void Awake()
    {
        character = GetComponent<Character>();
        frontCheck = transform.Find("frontCheck").transform;
    }

    void FixedUpdate ()
    {
        if (character.CanChangeOrientation())
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
                    Vector2 dir = character.GetOrientation();
                    dir.x *= -1f;
                    dir.y = 0f;
                    character.SetOrientation(dir);
                    break;
                }
            }
        }

        if (character.CanMove())
        {
            character.Move(character.GetOrientation().x);
        }
    }
}
