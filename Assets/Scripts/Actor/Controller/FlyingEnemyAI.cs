using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAI : MonoBehaviour
{
    public float        initialAltitude = 10f;

    public float        minAttackInterval = 0.5f;
    public float        maxAttackInterval = 1.5f;

    private Transform   frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
    private Character   character;

    private Character   player;

    void Awake()
    {
        character = GetComponent<Character>();
        character.SetHovering(true);
        character.hoverAltitude = initialAltitude;

        frontCheck = transform.Find("frontCheck").transform;

        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go != null)
            player = go.GetComponent<Character>();

        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(1.5f);
        while (true)
        {
            if (player != null && character.CanShoot())
            {
                character.Shoot(Aim());
                yield return new WaitForSeconds(Random.Range(minAttackInterval, maxAttackInterval));
            }
            yield return null;
        }
    }

    Vector2 Aim()
    {
        return (player.transform.position - transform.position).normalized;
    }

    void FixedUpdate()
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
