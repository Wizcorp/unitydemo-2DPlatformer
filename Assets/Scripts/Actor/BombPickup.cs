using UnityEngine;
using System.Collections;

public class BombPickup : Pickup
{
	public AudioClip pickupClip;		// Sound for when the bomb crate is picked up.

    protected override void OnPickedUp(GameObject gameObject)
    {
        base.OnPickedUp(gameObject);

        // ... play the pickup sound effect.
        AudioSource.PlayClipAtPoint(pickupClip, transform.position);

        // Increase the number of bombs the player has.
        gameObject.GetComponent<LayBombs>().bombCount++;
    }

    protected override void OnDied()
    {
        GetComponent<Bomb>().Explode();

        base.OnDied();
    }
}
