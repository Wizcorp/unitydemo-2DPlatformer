using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public AudioClip    fireSound;
    public Rigidbody2D  projectile;

    public virtual void Fire(Vector2 direction, string shooterTag)
    {
        if (fireSound)
            PlayFireSound();

        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, direction);
        Rigidbody2D bulletInstance = Instantiate(projectile, transform.position, rotation) as Rigidbody2D;

        Projectile projectileComp = bulletInstance.GetComponent<Projectile>();
        projectileComp.shooterTag = shooterTag;

        bulletInstance.velocity = direction * projectileComp.speed;
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
