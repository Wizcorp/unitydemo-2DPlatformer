using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public Rigidbody2D rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.

    //overHeat System
    public int maxAmmo = 25;
    public int currAmmo = 25;
    private GUITexture heatHUD;
    public float flashingTime = 0.25f;
    private bool overHeating = false;

	private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;                  // Reference to the Animator component.

    void Start(){
        heatHUD.enabled = false;
    }

	void Awake()
	{
		// Setting up the references.
		anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();
        heatHUD = GameObject.Find("ui_overHeatHUD").GetComponent<GUITexture>();
	}


	void Update ()
	{
        //check if the game is pause and if it is ignore user input
        if (PauseMenu.GameIsPaused == false)
        {
            //Overheating system
            //ignore input if overheat
            if (overHeating) return;
            //ammo count is there to check for overheat
            if (currAmmo <= 0)
            {
                StartCoroutine(overHeat());
                return;
            }

            // If the fire button is pressed...
            if (Input.GetButtonDown("Fire1"))
            {
                // ... set the animator Shoot trigger parameter and play the audioclip.
                anim.SetTrigger("Shoot");
                GetComponent<AudioSource>().Play();

                // If the player is facing right...
                if (playerCtrl.facingRight)
                {
                    // ... instantiate the rocket facing right and set it's velocity to the right. 
                    Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                    bulletInstance.velocity = new Vector2(speed, 0);
                }
                else
                {
                    // Otherwise instantiate the rocket facing left and set it's velocity to the left.
                    Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                    bulletInstance.velocity = new Vector2(-speed, 0);
                }
                currAmmo--;
            }
        }
	}
    //Overheat delay system
    IEnumerator overHeat()
    {
        overHeating = true;
        //flashing icon when overheat 0.25 X 10 = 2.5sec
        heatHUD.enabled = true;
        yield return new WaitForSeconds(flashingTime);
        heatHUD.enabled = false;
        yield return new WaitForSeconds(flashingTime);
        heatHUD.enabled = true;
        yield return new WaitForSeconds(flashingTime);
        heatHUD.enabled = false;
        yield return new WaitForSeconds(flashingTime);
        heatHUD.enabled = true;
        yield return new WaitForSeconds(flashingTime);
        heatHUD.enabled = true;
        yield return new WaitForSeconds(flashingTime);
        heatHUD.enabled = false;
        yield return new WaitForSeconds(flashingTime);
        heatHUD.enabled = true;
        yield return new WaitForSeconds(flashingTime);
        heatHUD.enabled = false;
        yield return new WaitForSeconds(flashingTime);
        heatHUD.enabled = true;
        yield return new WaitForSeconds(flashingTime);
        heatHUD.enabled = false;
        currAmmo = maxAmmo;
        overHeating = false;
    }
}
