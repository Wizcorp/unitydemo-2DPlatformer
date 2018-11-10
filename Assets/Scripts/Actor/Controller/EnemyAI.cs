using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform m_FrontCheck;
    private Character m_Character;

    void Awake()
    {
        m_Character = GetComponent<Character>();
        m_FrontCheck = transform.Find("frontCheck").transform;
    }

    void FixedUpdate ()
    {
        if (m_Character.CanChangeOrientation())
        {
            // Create an array of all the colliders in front of the enemy.
            Collider2D[] frontHits = Physics2D.OverlapPointAll(m_FrontCheck.position, 1);

            // Check each of the colliders.
            foreach (Collider2D c in frontHits)
            {
                // If any of the colliders is an Obstacle...
                if (c.tag == "Obstacle")
                {
                    // ... Flip the enemy and stop checking the other colliders.
                    Vector2 dir = m_Character.GetOrientation();
                    dir.x *= -1f;
                    dir.y = 0f;
                    m_Character.SetOrientation(dir);
                    break;
                }
            }
        }

        if (m_Character.CanMove())
        {
            m_Character.Move(m_Character.GetOrientation().x);
        }
    }
}
