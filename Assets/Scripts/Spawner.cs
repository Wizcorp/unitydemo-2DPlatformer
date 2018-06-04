using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public enum Direction
    {
        left = 0,
        right = 1,
        random = 2
    }

    public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.

    public Direction direction;

    public GameObject[] enemies;		// Array of enemy prefabs.


	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}


	void Spawn ()
	{
		// Instantiate a random enemy.
		int enemyIndex = Random.Range(0, enemies.Length);

        ChangeDirection(enemies[enemyIndex]);

        Instantiate(enemies[enemyIndex], transform.position ,transform.rotation);

		// Play the spawning effect from all of the particle systems.
		foreach(ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		{
			p.Play();
		}
	}

    public void ChangeDirection(GameObject ennemy)
    {
        Vector3 enemyScale;

        switch (direction)
        {
            case Direction.left:
                enemyScale = ennemy.transform.localScale;
                if (enemyScale.x > 0)
                {
                    enemyScale.x *= -1;
                    ennemy.transform.localScale = enemyScale;
                }
                break;
            case Direction.right:
                break;
            case Direction.random:
            default:
                int a = Random.Range(0, 2);
                enemyScale = ennemy.transform.localScale;
                if (a == 1)
                {
                    if (enemyScale.x > 0)
                        enemyScale.x *= -1;
                }
                else
                {
                    if (enemyScale.x < 0)
                        enemyScale.x *= -1;
                }
                ennemy.transform.localScale = enemyScale;
                break;
        }
        // Multiply the x component of localScale by -1.
        
    }
}
