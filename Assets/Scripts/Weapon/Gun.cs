using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.

    public void Fire(Vector2 direction)
    {
        GetComponent<AudioSource>().Play();

        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, direction);
        Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, rotation) as Rigidbody2D;
        bulletInstance.velocity = direction * speed;
    }
}
