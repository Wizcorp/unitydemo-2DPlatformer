using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Character m_Character;

	void Awake()
	{
        m_Character = GetComponent<Character>();
	}

	void Update()
	{
        if (m_Character.CanChangeOrientation())
        {
            m_Character.SetOrientation(GetAimDirection());
        }

        if (m_Character.CanShoot() && Input.GetButtonDown("Fire1"))
        {
            m_Character.Shoot(m_Character.GetOrientation());
        }

        if (m_Character.CanUseBomb() && Input.GetButtonDown("Fire2"))
        {
            m_Character.UseBomb();
        }

        if (m_Character.CanJump() && Input.GetButtonDown("Jump"))
        {
            m_Character.Jump(Vector2.up);
        }

        float h = Input.GetAxis("Horizontal");
        if (m_Character.CanMove())
        {
            m_Character.Move(h);
        }
    }

    Vector2 GetAimDirection()
    {
        RangedWeapon rangedWeapon = m_Character.GetRangedWeapon();
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        v -= rangedWeapon.transform.position;
        v.z = 0.0f;
        v.Normalize();

        if (v.sqrMagnitude == 0f)
        {
            return m_Character.GetOrientation();
        }
        else
        {
            return v;
        }
    }
}
