using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour 
{
    public float        damage = 1f;
	public GameObject   explosion;

	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 2);
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
        if (col.tag == "Player")
            return;

        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        Instantiate(explosion, transform.position, randomRotation);

        Actor actor = col.gameObject.GetComponent<Actor>();
        if (actor != null)
        {
            ActorEffect effect;
            effect.type = ActorEffect.Type.Damage;
            effect.amount = damage;
            effect.forceVector = Vector2.zero;

            actor.ApplyEffect(effect);
        }

        Destroy(gameObject);
    }
}
