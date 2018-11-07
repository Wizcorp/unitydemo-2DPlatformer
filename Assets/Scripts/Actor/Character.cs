using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    public float    jumpForce = 1000f;         // Amount of force added when the player jumps.

    public float    moveForce = 365f;          // Amount of force added to move the player left and right.
    public float    maxSpeed = 5f;				// The fastest the player can travel in the x axis.

    private const float maxAltitude = 14f;

    public float    hoverAltitude = 10f;
    public float    hoverForce = 15f;
    public float    maxHoverSpeed = 3f;

    private Vector2 orientation = Vector2.right;

    private bool    isOnGround = false;
    private bool    isJumping = false;
    private bool    isHovering = false;

    private Vector2 groundNormal;

    private float   movement = 0f;
    private bool    inFixedUpdate = false;

    private RangedWeapon rangedWeapon;

    private LayBombs layBombs; //temporary

    protected override void Awake()
    {
        base.Awake();
        layBombs = GetComponentInChildren<LayBombs>();
        rangedWeapon = GetComponentInChildren<RangedWeapon>();
        if (rangedWeapon)
            rangedWeapon.shooterTag = gameObject.tag;
    }

    protected RangedWeapon GetRangedWeapon()
    {
        return rangedWeapon;
    }
	
	protected virtual void FixedUpdate ()
    {
        inFixedUpdate = true;
        if (isAlive)
        {
            UpdateGround();

            if (isJumping && isOnGround)
                isJumping = false;

            if (Mathf.Abs(movement) > Mathf.Epsilon)
                DoMove();

            if (isHovering)
                DoHover();
        }
    }

    protected virtual void Update()
    {
        inFixedUpdate = false;
    }

    protected Vector2 GetGroundNormal()
    {
        return groundNormal;
    }

    void UpdateGround()
    {
        Vector3 startPos = transform.position + Vector3.up * 0.5f;
        Vector3 endPos = startPos + Vector3.down * 2.0f;
        RaycastHit2D hit = Physics2D.Linecast(startPos, endPos, 1 << LayerMask.NameToLayer("Ground"));
        isOnGround = hit;
        if (hit)
            groundNormal = hit.normal;
        else
            groundNormal = Vector2.up;
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


    public bool CanHover()
    {
        return isAlive;
    }

    public virtual void SetHovering(bool hovering)
    {
        Debug.Assert(CanHover());

        bool wasHovering = isHovering;
        isHovering = hovering;

        if (isHovering && !wasHovering && inFixedUpdate)
            DoHover();
    }

    public bool IsHovering() { return isHovering; }

    void DoHover()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();

        Vector2 antigravity = -Physics2D.gravity * body.mass;
        Vector2 velocity = body.velocity;

        body.AddForce(antigravity * 0.85f);

        if (transform.position.y < hoverAltitude)
        {
            float distance = Mathf.Abs(hoverAltitude - transform.position.y);
            float distanceFactor = Mathf.Min(distance, 1f);
            body.AddForce(Vector2.up * hoverForce * distanceFactor);
        }

        if (Mathf.Abs(velocity.y) > maxHoverSpeed)
        {
            velocity.y = Mathf.Sign(velocity.y) * maxHoverSpeed;
            body.velocity = velocity;
        }

        if (transform.position.y > maxAltitude)
        {
            transform.position = new Vector2(transform.position.x, maxAltitude);
            velocity.y = 0f;
            body.velocity = velocity;
        }

        // If linear drag is zero, this causes the character to stop in the air
        // after half a second if there is no other force applied
        if (Mathf.Abs(velocity.x) > 0f && body.drag == 0f)
        {
            float timeToStop = 0.5f;
            float stopForce = body.mass * Mathf.Abs(velocity.x) / timeToStop;
            body.AddForce(Vector2.left * Mathf.Sign(velocity.x) * stopForce);
        }
    }

    public bool CanMove()
    {
        return isAlive;
    }

    public virtual void Move(float moveVector)
    {
        Debug.Assert(CanMove());
        movement = moveVector;

        if (inFixedUpdate)
            DoMove();
    }

    void DoMove()
    { 
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        Vector2 velocity = body.velocity;

        if (Mathf.Abs(movement) > Mathf.Epsilon )
        {
            float velocityInDirection = movement * velocity.x;
            if (velocityInDirection < maxSpeed)
            {
                body.AddForce(Vector2.right * movement * moveForce);
            }
        }

        if (Mathf.Abs(velocity.x) > maxSpeed)
        {
            body.velocity = new Vector2(Mathf.Sign(velocity.x) * maxSpeed, velocity.y);
        }

        movement = 0f;
    }

    public bool CanJump()
    {
        return isAlive && isOnGround && !isJumping && !isHovering;
    }

    public virtual void Jump(Vector2 direction)
    {
        Debug.Assert(CanJump());

        isJumping = true;
        GetComponent<Rigidbody2D>().AddForce(direction * jumpForce);
    }

    public bool CanShoot()
    {
        return isAlive && rangedWeapon != null && rangedWeapon.CanFire();
    }

    public virtual void Shoot(Vector2 direction)
    {
        Debug.Assert(CanShoot());

        rangedWeapon.Fire(direction);
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
