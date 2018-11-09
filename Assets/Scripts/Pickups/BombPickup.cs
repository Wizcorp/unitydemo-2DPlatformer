using UnityEngine;
using System.Collections;

public class BombPickup : Pickup
{
    protected override bool OnTryPickUp(Character character)
    {
        BombContainer container = character.GetComponentInChildren<BombContainer>();
        if (!container)
            return false;

        return container.AddBomb();
    }

    protected override void OnDied()
    {
        GetComponent<Bomb>().Explode();

        base.OnDied();
    }
}
