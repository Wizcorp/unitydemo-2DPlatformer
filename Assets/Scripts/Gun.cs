using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.
    public float fireRate = 0.5f;           // The rate the rocket can fire at (to prevent spam).
    private float nextFireTime = 0f;        // Counter to keep track of the next time the rocket can fire.
    private Quaternion gunRotation;         // Rotation of the gun.


    private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;                  // Reference to the Animator component.

    private Camera cam;                      //edit Reference to the Camera to get the players screen position

    void Awake()
	{
		// Setting up the references.
		anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();
        cam = Camera.main;
    }


	void Update ()
	{
        // Rotate the gun to look at the mouse
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position); // Get the guns position in screenspace
        Vector3 relativePos = Input.mousePosition - screenPos;   // Get the vector from mouse position to the player position
        relativePos.Normalize();                                            //Normalize the vector

        // Get the rotation of the vector from player position to mouse position
        float rot_z = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);                       // Set the rotation to look at the mouse position

        // If the player is facing left flip the orientation of the gun
        if (!playerCtrl.facingRight)
        {
            transform.localScale = new Vector3(-0.5f, -0.5f, 0.5f);
        }
        else
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        };

        // If the fire button is pressed and not on cooldown...
        if (Input.GetButtonDown("Fire1") && Time.time > nextFireTime)
        {
            // ... set the animator Shoot trigger parameter and play the audioclip.
            anim.SetTrigger("Shoot");
            GetComponent<AudioSource>().Play();


            // ... instantiate the rocket rotation and it's velocity toward the mouse position. 
            Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, transform.rotation) as Rigidbody2D;
            bulletInstance.velocity = speed * transform.right;

            // Set the cooldown by adding fire rate to current time
            nextFireTime = Time.time + fireRate;
        }
    }
}
