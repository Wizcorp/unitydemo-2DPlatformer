using UnityEngine;
using System.Collections;

public class Rocket : Projectile 
{
	public GameObject explosion;

	protected override void OnHit(Collider2D col) 
	{
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        Instantiate(explosion, transform.position, randomRotation);
    }
}
