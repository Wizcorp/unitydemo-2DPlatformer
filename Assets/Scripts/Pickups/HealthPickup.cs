using UnityEngine;
using System.Collections;

public class HealthPickup : Pickup
{
	public float healthBonus;				// How much health the crate gives the player.

	private PickupSpawner pickupSpawner;	// Reference to the pickup spawner.

	protected override void Awake ()
	{
        base.Awake();

		// Setting up the references.
		pickupSpawner = GameObject.Find("pickupManager").GetComponent<PickupSpawner>();
	}

    protected override bool OnTryPickUp(Character character)
    {
        ActorEffect effect;
        effect.type = ActorEffect.Type.Heal;
        effect.amount = healthBonus;
        effect.forceVector = Vector2.zero;

        character.ApplyEffect(effect);

        // Trigger a new delivery.
        pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());

        return true;
    }

    protected override void OnDied()
    {
        base.OnDied();
        pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());
    }
}
