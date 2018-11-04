using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ActorEffect
{
    public enum Type
    {
        Damage,
        Kill,
        Heal
    }

    public Type     type;
    public float    amount;
    public Vector2  forceVector;
}
