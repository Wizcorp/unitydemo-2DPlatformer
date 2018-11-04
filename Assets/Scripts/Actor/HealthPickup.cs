using UnityEngine;
using System.Collections;

public class HealthPickup : Pickup
{
	public float healthBonus;				// How much health the crate gives the player.
	public AudioClip collect;				// The sound of the crate being collected.

	private PickupSpawner pickupSpawner;	// Reference to the pickup spawner.

	protected override void Awake ()
	{
        base.Awake();

		// Setting up the references.
		pickupSpawner = GameObject.Find("pickupManager").GetComponent<PickupSpawner>();
	}

    protected override void OnPickedUp(GameObject gameObject)
    {
        base.OnPickedUp(gameObject);

        ActorEffect effect;
        effect.type = ActorEffect.Type.Heal;
        effect.amount = healthBonus;
        effect.forceVector = Vector2.zero;

        gameObject.GetComponent<Actor>().ApplyEffect(effect);

        // Trigger a new delivery.
        pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());

        // Play the collection sound.
        AudioSource.PlayClipAtPoint(collect, transform.position);
    }
}
