using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Character   m_Character;
    private Vector2     m_LastMousePosition = Vector2.zero;
    private bool        m_UsingMouse = false;

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
        if (m_LastMousePosition != (Vector2)Input.mousePosition)
        {
            m_LastMousePosition = Input.mousePosition;
            m_UsingMouse = true;
        }

        float aimX = Input.GetAxis("AimX");
        float aimY = Input.GetAxis("AimY");

        if (aimX != 0f || aimY != 0f)
        {
            m_UsingMouse = false;
            return new Vector2(aimX, aimY).normalized;
        }

        if (m_UsingMouse)
        {
            RangedWeapon rangedWeapon = m_Character.GetRangedWeapon();
            Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            v -= (Vector2)rangedWeapon.transform.position;
            return v.normalized;
        }
        else
        {
            Vector2 v = m_Character.GetOrientation();
            float h = Input.GetAxis("Horizontal");
            if (h != 0f)
                v.x = Mathf.Sign(h);

            return v.x > 0f ? Vector2.right : Vector2.left;
        }
    }
}
