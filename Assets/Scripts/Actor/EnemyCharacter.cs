using UnityEngine;
using System.Collections;

public class EnemyCharacter : Character
{
    public float        damage = 10f;
    public float        minDamageInterval = 0.2f;
    public float        pushForce = 10f;
    public Sprite       deadEnemy;              // A sprite of the enemy when it's dead.
    public Sprite       damagedEnemy;           // An optional sprite of the enemy when it's damaged.
    public AudioClip[]  deathClips;             // An array of audioclips that can play when the enemy dies.
    public float        deathSpinMin = -100f;   // A value to give the minimum amount of Torque when dying
    public float        deathSpinMax = 100f;	// A value to give the maximum amount of Torque when dying
    public GameObject   hundredPointsUI;        // A prefab of 100 that appears when the enemy dies.

    private SpriteRenderer  m_SpriteRenderer;
    private Animator        m_Animator;
    private Score           m_Score;

    private GameObject      m_CollidingPlayer;
    private bool            m_NeedApplyPushForce = false;
    private float           m_LastDamageTime = 0f;

    protected override void Awake()
    {
        base.Awake();

        m_SpriteRenderer = transform.Find("body").GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
        m_Score = GameObject.Find("Score").GetComponent<Score>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        transform.localRotation = Quaternion.FromToRotation(Vector2.up, groundNormal);
        m_CollidingPlayer = null;
    }

    protected override void Update()
    {
        base.Update();

        if (m_CollidingPlayer)
        {
            TryApplyDamage();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && !m_CollidingPlayer)
        {
            m_CollidingPlayer = col.gameObject;
            m_NeedApplyPushForce = true;
            TryApplyDamage();
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
            m_CollidingPlayer = col.gameObject;
    }

    void TryApplyDamage()
    {
        if ((Time.time - m_LastDamageTime) < minDamageInterval)
            return;

        Vector3 knockUpForce = m_CollidingPlayer.transform.position - transform.position
            + Vector3.up * 5f;

        ActorEffect effect;
        effect.type = ActorEffect.Type.Damage;
        effect.amount = damage;
        effect.forceVector = Vector3.zero;

        if (m_NeedApplyPushForce)
        {
            effect.forceVector = knockUpForce * pushForce;
        }

        m_CollidingPlayer.GetComponent<Actor>().ApplyEffect(effect);

        m_NeedApplyPushForce = false;
        m_LastDamageTime = Time.time;
    }

    public override void Move(float moveVector)
    {
        base.Move(moveVector);

        float v = GetComponent<Rigidbody2D>().velocity.x;
        m_Animator.SetFloat("Speed", Mathf.Abs(v));
    }

    protected override void OnDamageReceived(float amount)
    {
        bool wasDamaged = health < totalHealth;

        base.OnDamageReceived(amount);

        if (health > 0f && health < totalHealth)
        {
            if (damagedEnemy && !wasDamaged)
            {
                m_SpriteRenderer.sprite = damagedEnemy;
            }
        }
    }

    protected override void OnDied()
	{
        base.OnDied();

		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

		// Disable all of them sprite renderers.
		foreach(SpriteRenderer s in otherRenderers)
		{
			s.enabled = false;
		}

        // Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
        m_SpriteRenderer.enabled = true;
        m_SpriteRenderer.sprite = deadEnemy;

        // Increase the score by 100 points
        m_Score.score += 100;

		// Allow the enemy to rotate and spin it by adding a torque.
		GetComponent<Rigidbody2D>().fixedAngle = false;
		GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin,deathSpinMax));

		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
		}

		// Play a random audioclip from the deathClips array.
		int i = Random.Range(0, deathClips.Length);
		AudioSource.PlayClipAtPoint(deathClips[i], transform.position);

		// Create a vector that is just above the enemy.
		Vector3 scorePos;
		scorePos = transform.position;
		scorePos.y += 1.5f;

		// Instantiate the 100 points prefab at this point.
		Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
	}
}
