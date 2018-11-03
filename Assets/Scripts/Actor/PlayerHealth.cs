using UnityEngine;
using System.Collections;

using Actor;

public class PlayerHealth : ActorBase
{	
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public AudioClip[] ouchClips;				// Array of clips to play when the player is damaged.
	public float hurtForce = 10f;				// The force with which the player is pushed when hurt.
	public float damageAmount = 10f;			// The amount of damage to take when enemies touch the player

	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	private PlayerControl playerControl;		// Reference to the PlayerControl script.
	private Animator anim;						// Reference to the Animator on the player


	protected override void Awake ()
	{
        base.Awake();

		// Setting up references.
		playerControl = GetComponent<PlayerControl>();
		healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();

		// Getting the intial scale of the healthbar (whilst the player has full health).
		healthScale = healthBar.transform.localScale;
	}

    protected override void OnDamageReceived(float amount)
    {
        base.OnDamageReceived(amount);

        // Make sure the player can't jump.
        playerControl.jump = false;

        UpdateHealthBar();

        if (health > 0f)
        {
            // Play a random clip of the player getting hurt.
            int i = Random.Range(0, ouchClips.Length);
            AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
        }
    }

    protected override void OnHealReceived(float amount)
    {
        base.OnHealReceived(amount);
        UpdateHealthBar();
    }

    protected override void OnDied()
    {
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

        // ... disable user Player Control script
        GetComponent<PlayerControl>().enabled = false;

        // ... disable the Gun script to stop a dead guy shooting a nonexistant bazooka
        GetComponentInChildren<Gun>().enabled = false;

        // ... Trigger the 'Die' animation state
        anim.SetTrigger("Die");
    }

	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
	}
}
