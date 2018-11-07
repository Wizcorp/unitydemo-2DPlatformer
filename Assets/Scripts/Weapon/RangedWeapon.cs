using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public AudioClip    fireSound;
    public Rigidbody2D  projectile;
    public float        reloadTime = 0f;

    [HideInInspector]
    public string       shooterTag;

    private Transform   muzzleTransform;
    private float       lastShotTime = 0f;

    protected virtual void Awake()
    {
        muzzleTransform = transform.Find("muzzle");
        if (!muzzleTransform)
            muzzleTransform = transform;
    }

    public virtual bool CanFire()
    {
        return (Time.time - lastShotTime) >= reloadTime;
    }

    public virtual void Fire(Vector2 direction)
    {
        Debug.Assert(CanFire());

        if (fireSound)
            PlayFireSound();

        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, direction);
        Rigidbody2D bulletInstance = Instantiate(projectile, muzzleTransform.position, rotation) as Rigidbody2D;

        Projectile projectileComp = bulletInstance.GetComponent<Projectile>();
        projectileComp.shooterTag = shooterTag;

        bulletInstance.velocity = direction * projectileComp.speed;

        lastShotTime = Time.time;
    }

    void PlayFireSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource)
        {
            audioSource.PlayOneShot(fireSound);
        }
        else
        {
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
        }
    }
}
