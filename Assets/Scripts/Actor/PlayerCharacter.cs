using UnityEngine;
using System.Collections;

public class PlayerCharacter : Character
{	
	public AudioClip[]  ouchClips;          // Array of clips to play when the player is damaged.
    public AudioClip[]  jumpClips;          // Array of clips for when the player jumps.
    public AudioClip    bombsAway;          // Sound for when the player lays a bomb.

    public AudioClip[]  taunts;                     // Array of clips for when the player taunts.
    public float        tauntProbability = 50f;     // Chance of a taunt happening.
    public float        tauntDelay = 1f;            // Delay for when the taunt should happen.

    private Animator    m_Animator;
    private int         m_TauntIndex;

    protected override void Awake ()
	{
        base.Awake();

        m_Animator = GetComponent<Animator>();
	}

    public override void SetOrientation(Vector2 direction)
    {
        base.SetOrientation(direction);

        // Limit the rotation of the weapon to the range from 45 degrees down to 45 degrees up
        RangedWeapon rangedWeapon = GetRangedWeapon();
        if (rangedWeapon)
        {
            // TODO: This is a bit unreadable
            Vector2 dir = new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
            if (dir.x < dir.y)              // if it is above 45 degrees
                dir.x = dir.y = 0.7071f;    // set 'dir' to Normalized(1,1)

            dir.y *= Mathf.Sign(direction.y);
            rangedWeapon.transform.localRotation = Quaternion.FromToRotation(Vector2.right, dir);
        }
    }

    public override void Move(float moveVector)
    {
        base.Move(moveVector);

        m_Animator.SetFloat("Speed", Mathf.Abs(moveVector));
    }

    public override void Shoot(Vector2 direction)
    {
        base.Shoot(direction);

        m_Animator.SetTrigger("Shoot");
    }

    public override void Jump(Vector2 direction)
    {
        base.Jump(direction);

        m_Animator.SetTrigger("Jump");

        int i = Random.Range(0, jumpClips.Length);
        AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
    }

    public override void UseBomb()
    {
        base.UseBomb();

        AudioSource.PlayClipAtPoint(bombsAway, transform.position);
    }

    protected override void OnDamageReceived(float amount)
    {
        base.OnDamageReceived(amount);

        if (health > 0f)
        {
            int i = Random.Range(0, ouchClips.Length);
            AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
        }
    }

    protected override void OnDied()
    {
        base.OnDied();

        // Find all of the colliders on the gameobject and set them all to be triggers.
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
        }

        // Move all sprite parts of the player to the front
        SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in spr)
        {
            s.sortingLayerName = "UI";
        }

        // ... Trigger the 'Die' animation state
        m_Animator.SetTrigger("Die");
    }

    public IEnumerator Taunt()
    {
        // Check the random chance of taunting.
        float tauntChance = Random.Range(0f, 100f);
        if (tauntChance > tauntProbability)
        {
            // Wait for tauntDelay number of seconds.
            yield return new WaitForSeconds(tauntDelay);

            // If there is no clip currently playing.
            if (!GetComponent<AudioSource>().isPlaying)
            {
                // Choose a random, but different taunt.
                m_TauntIndex = TauntRandom();

                // Play the new taunt.
                GetComponent<AudioSource>().clip = taunts[m_TauntIndex];
                GetComponent<AudioSource>().Play();
            }
        }
    }


    int TauntRandom()
    {
        // Choose a random index of the taunts array.
        int i = Random.Range(0, taunts.Length);

        // If it's the same as the previous taunt...
        if (i == m_TauntIndex)
            // ... try another random taunt.
            return TauntRandom();
        else
            // Otherwise return this index.
            return i;
    }
}
