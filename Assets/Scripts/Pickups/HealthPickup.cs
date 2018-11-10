using UnityEngine;
using System.Collections;

public class HealthPickup : Pickup
{
	public float healthBonus;				// How much health the crate gives the player.

	private PickupSpawner m_PickupSpawner;	// Reference to the pickup spawner.

	protected override void Awake ()
	{
        base.Awake();

        m_PickupSpawner = GameObject.Find("pickupManager").GetComponent<PickupSpawner>();
	}

    protected override bool OnTryPickUp(Character character)
    {
        ActorEffect effect;
        effect.type = ActorEffect.Type.Heal;
        effect.amount = healthBonus;
        effect.forceVector = Vector2.zero;

        character.ApplyEffect(effect);

        // Trigger a new delivery.
        m_PickupSpawner.StartCoroutine(m_PickupSpawner.DeliverPickup());

        return true;
    }

    protected override void OnDied()
    {
        base.OnDied();

        m_PickupSpawner.StartCoroutine(m_PickupSpawner.DeliverPickup());
    }
}
