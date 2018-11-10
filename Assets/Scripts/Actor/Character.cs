using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    private const float MaxAltitude = 14f;

    public float    jumpForce = 1000f;         // Amount of force added when the player jumps.
    public float    moveForce = 365f;          // Amount of force added to move the player left and right.
    public float    maxSpeed = 5f;				// The fastest the player can travel in the x axis.
    public float    hoverAltitude = 10f;
    public float    hoverForce = 15f;
    public float    maxHoverSpeed = 3f;

    private Vector2 m_Orientation = Vector2.right;

    private bool    m_IsOnGround = false;
    private bool    m_IsJumping = false;
    private bool    m_IsHovering = false;

    private Vector2 m_GroundNormal = Vector2.up;

    private float   m_Movement = 0f;
    private bool    m_InFixedUpdate = false;

    private RangedWeapon    m_RangedWeapon = null;
    private BombContainer   m_BombContainer = null;
    private GameObject      m_ActiveBomb;

    protected Vector2 groundNormal
    {
        get { return m_GroundNormal; }
    }

    protected override void Awake()
    {
        base.Awake();

        m_RangedWeapon = GetComponentInChildren<RangedWeapon>();
        if (m_RangedWeapon)
            m_RangedWeapon.shooterTag = gameObject.tag;

        m_BombContainer = GetComponentInChildren<BombContainer>();
    }

	protected virtual void FixedUpdate ()
    {
        m_InFixedUpdate = true;
        if (isAlive)
        {
            UpdateGround();

            if (m_IsJumping && m_IsOnGround)
                m_IsJumping = false;

            if (Mathf.Abs(m_Movement) > Mathf.Epsilon)
                DoMove();

            if (m_IsHovering)
                DoHover();
        }
    }

    protected virtual void Update()
    {
        m_InFixedUpdate = false;
    }

    void UpdateGround()
    {
        Vector3 startPos = transform.position + Vector3.up * 0.5f;
        Vector3 endPos = startPos + Vector3.down * 2.0f;
        RaycastHit2D hit = Physics2D.Linecast(startPos, endPos, 1 << LayerMask.NameToLayer("Ground"));
        m_IsOnGround = hit;
        if (hit)
            m_GroundNormal = hit.normal;
        else
            m_GroundNormal = Vector2.up;
    }

    public bool CanChangeOrientation()
    {
        return isAlive;
    }

    public virtual void SetOrientation(Vector2 direction)
    {
        m_Orientation = direction;

        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Sign(m_Orientation.x) * Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }

    public Vector2 GetOrientation() { return m_Orientation; }


    public bool CanHover()
    {
        return isAlive;
    }

    public virtual void SetHovering(bool hovering)
    {
        Debug.Assert(CanHover());

        bool wasHovering = m_IsHovering;
        m_IsHovering = hovering;

        if (m_IsHovering && !wasHovering && m_InFixedUpdate)
            DoHover();
    }

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

        if (transform.position.y > MaxAltitude)
        {
            transform.position = new Vector2(transform.position.x, MaxAltitude);
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
        m_Movement = moveVector;

        if (m_InFixedUpdate)
            DoMove();
    }

    void DoMove()
    { 
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        Vector2 velocity = body.velocity;

        if (Mathf.Abs(m_Movement) > Mathf.Epsilon )
        {
            float velocityInDirection = m_Movement * velocity.x;
            if (velocityInDirection < maxSpeed)
            {
                body.AddForce(Vector2.right * m_Movement * moveForce);
            }
        }

        if (Mathf.Abs(velocity.x) > maxSpeed)
        {
            body.velocity = new Vector2(Mathf.Sign(velocity.x) * maxSpeed, velocity.y);
        }

        m_Movement = 0f;
    }

    public bool CanJump()
    {
        return isAlive && m_IsOnGround && !m_IsJumping && !m_IsHovering;
    }

    public virtual void Jump(Vector2 direction)
    {
        Debug.Assert(CanJump());

        m_IsJumping = true;
        GetComponent<Rigidbody2D>().AddForce(direction * jumpForce);
    }

    public bool CanShoot()
    {
        return isAlive && m_RangedWeapon && m_RangedWeapon.CanFire();
    }

    public virtual void Shoot(Vector2 direction)
    {
        Debug.Assert(CanShoot());

        m_RangedWeapon.Fire(direction);
    }

    public RangedWeapon GetRangedWeapon()
    {
        return m_RangedWeapon;
    }

    public bool CanUseBomb()
    {
        return isAlive && m_BombContainer && m_BombContainer.HasBombs() && !m_ActiveBomb;
    }

    public virtual void UseBomb()
    {
        Debug.Assert(CanUseBomb());

        m_ActiveBomb = m_BombContainer.TakeBomb(transform.position);
    }
}
