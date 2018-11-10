using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAI : MonoBehaviour
{
    public float        initialAltitude = 10f;

    public float        minAttackInterval = 0.5f;
    public float        maxAttackInterval = 1.5f;

    private Transform   m_FrontCheck;
    private Character   m_Character;

    private Character   m_PlayerCharacter;

    void Awake()
    {
        m_Character = GetComponent<Character>();
        m_Character.SetHovering(true);
        m_Character.hoverAltitude = initialAltitude;

        m_FrontCheck = transform.Find("frontCheck").transform;

        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go != null)
            m_PlayerCharacter = go.GetComponent<Character>();

        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(1.5f);
        while (true)
        {
            if (m_PlayerCharacter && m_Character.CanShoot())
            {
                m_Character.Shoot(Aim());
                yield return new WaitForSeconds(Random.Range(minAttackInterval, maxAttackInterval));
            }
            yield return null;
        }
    }

    Vector2 Aim()
    {
        return (m_PlayerCharacter.transform.position - transform.position).normalized;
    }

    void FixedUpdate()
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
