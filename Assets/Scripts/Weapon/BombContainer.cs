using UnityEngine;
using System.Collections;

public class BombContainer : MonoBehaviour
{
    public Bomb bombType;
    private int bombCount = 0;

    public bool AddBomb()
    {
        ++bombCount;
        return true;
    }

    public bool HasBombs() { return bombCount > 0; }

    public GameObject TakeBomb(Vector2 position)
    {
        Debug.Assert(HasBombs());

        --bombCount;
        return Instantiate(bombType.gameObject, position, Quaternion.identity);
    }
}
