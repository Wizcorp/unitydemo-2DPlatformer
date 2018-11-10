using UnityEngine;
using System.Collections;

public class BombContainer : MonoBehaviour
{
    public Bomb bombType;

    private int m_BombCount = 0;

    public int bombCount
    {
        get { return m_BombCount; }
    }

    public bool AddBomb()
    {
        ++m_BombCount;
        return true;
    }

    public bool HasBombs()
    {
        return m_BombCount > 0;
    }

    public GameObject TakeBomb(Vector2 position)
    {
        Debug.Assert(HasBombs());

        --m_BombCount;
        return Instantiate(bombType.gameObject, position, Quaternion.identity);
    }
}
