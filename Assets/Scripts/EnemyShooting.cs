using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

    public GameObject player;           // Reference to the player object
    public Rigidbody2D rocket;			// Prefab of the rocket.
    public float range;                 // Range the enemy will try to shoot the player
    public float aimRandomness = 2f;    // Value for some variation of bullets fired at player
    public float fireRate = 0.2f;       // Rate at which enemy can fire. 
    public float speed = 20f;           // The speed the rocket will fire at
    public GameObject firePoint;        // Point to fire rocket from
    private float nextFireTime = 0f;    // Counter to keep track of the next time the rocket can fire

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // If player is alive, in range and firing not on cooldown... 
        if (player != null
            && (player.transform.position.x > transform.position.x - range) 
            && (player.transform.position.x < transform.position.x + range) 
            && Time.time > nextFireTime)
        {
            // Shoot at the player with some randomness in height 
            float rand = Random.Range(aimRandomness, -aimRandomness); 
            Vector2 target = new Vector2(player.transform.position.x + rand, player.transform.position.y);
            Vector2 pos = new Vector2(firePoint.transform.position.x, firePoint.transform.position.y);
            Vector2 trajectory = target - pos;
            trajectory.Normalize();
            float rot_z = Mathf.Atan2(trajectory.y, trajectory.x) * Mathf.Rad2Deg;             // Get the roation between firepoint and target
            Quaternion rocketRotation = Quaternion.Euler(0f, 0f, rot_z);                       // Set the rotation to face target
            Rigidbody2D bulletInstance = Instantiate(rocket, firePoint.transform.position, rocketRotation) as Rigidbody2D;
            bulletInstance.velocity = speed * trajectory;
            nextFireTime = Time.time + fireRate;                        // Set next valid fire time
        }
	}
}
