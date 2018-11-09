using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Character character;

	void Awake()
	{
        character = GetComponent<Character>();
	}

	void Update()
	{
        if (character.CanChangeOrientation())
        {
            character.SetOrientation(GetAimDirection());
        }

        if (character.CanShoot() && Input.GetButtonDown("Fire1"))
        {
            character.Shoot(character.GetOrientation());
        }

        if (character.CanUseBomb() && Input.GetButtonDown("Fire2"))
        {
            character.UseBomb();
        }

        if (character.CanJump() && Input.GetButtonDown("Jump"))
        {
            character.Jump(Vector2.up);
        }

        float h = Input.GetAxis("Horizontal");
        if (character.CanMove())
        {
            character.Move(h);
        }
    }

    Vector2 GetAimDirection()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        v -= transform.position;
        v.z = 0.0f;
        v.Normalize();

        if (v.sqrMagnitude == 0f)
        {
            return character.GetOrientation();
        }
        else
        {
            return v;
        }
    }
}
