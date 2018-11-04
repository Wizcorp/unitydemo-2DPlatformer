using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    public float    jumpForce = 1000f;         // Amount of force added when the player jumps.

    public float    moveForce = 365f;          // Amount of force added to move the player left and right.
    public float    maxSpeed = 5f;				// The fastest the player can travel in the x axis.

    private Vector2 orientation = Vector2.right;

    private bool    isOnGround = false;
    private bool    isJumping = false;

    private Vector2 movement = Vector2.zero;
    private bool    inFixedUpdate = false;

    private LayBombs layBombs; //temporary

    protected override void Awake()
    {
        base.Awake();
        layBombs = GetComponentInChildren<LayBombs>();
    }
	
	protected virtual void FixedUpdate ()
    {
        inFixedUpdate = true;
        if (isAlive)
        {
            isOnGround = CheckOnGround();

            if (isJumping && isOnGround)
                isJumping = false;

            if (Mathf.Abs(movement.x) > Mathf.Epsilon)
                DoMove();
        }
    }

    protected virtual void Update()
    {
        inFixedUpdate = false;
    }

    bool CheckOnGround()
    {
        Vector3 startPos = transform.position + Vector3.up * 0.5f;
        Vector3 endPos = startPos + Vector3.down * 2.0f;
        return Physics2D.Linecast(startPos, endPos, 1 << LayerMask.NameToLayer("Ground"));
    }

    public bool CanChangeOrientation()
    {
        return isAlive;
    }

    public virtual void SetOrientation(Vector2 direction)
    {
        orientation = direction;

        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Sign(orientation.x) * Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }

    public Vector2 GetOrientation() { return orientation; }

    public bool CanMove()
    {
        return isAlive;
    }

    public virtual void Move(float moveVector)
    {
        Debug.Assert(CanMove());
        movement = new Vector2(moveVector, 0f);

        if (inFixedUpdate)
            DoMove();
    }

    void DoMove()
    { 
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        Vector2 velocity = body.velocity;

        float moveDir = movement.x;

        if (Mathf.Abs(moveDir) > Mathf.Epsilon )
        {
            float velocityInDirection = moveDir * velocity.x;

            if (velocityInDirection < maxSpeed)
            {
                body.AddForce(Vector2.right * moveDir * moveForce);
            }
        }

        if (Mathf.Abs(velocity.x) > maxSpeed)
        {
            body.velocity = new Vector2(Mathf.Sign(velocity.x) * maxSpeed, velocity.y);
        }

        movement = Vector2.zero;
    }

    public bool CanJump()
    {
        return isAlive && isOnGround && !isJumping;
    }

    public virtual void Jump(Vector2 direction)
    {
        Debug.Assert(CanJump());

        isJumping = true;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
    }

    public bool CanShoot()
    {
        return isAlive;
    }

    public virtual void Shoot(Vector2 direction)
    {
        Debug.Assert(CanShoot());

        GetComponentInChildren<Gun>().Fire(direction);
    }

    public bool CanUseBomb()
    {
        return isAlive && layBombs.CanUseBomb();
    }

    public virtual void UseBomb()
    {
        layBombs.UseBomb();
    }
}
