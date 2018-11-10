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

    private Transform   m_MuzzleTransform;
    private float       m_LastShotTime = 0f;

    protected virtual void Awake()
    {
        m_MuzzleTransform = transform.Find("muzzle");
        if (!m_MuzzleTransform)
            m_MuzzleTransform = transform;
    }

    public virtual bool CanFire()
    {
        return (Time.time - m_LastShotTime) >= reloadTime;
    }

    public virtual void Fire(Vector2 direction)
    {
        Debug.Assert(CanFire());

        if (fireSound)
            PlayFireSound();

        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, direction);
        Rigidbody2D bulletInstance = Instantiate(projectile, m_MuzzleTransform.position, rotation) as Rigidbody2D;

        Projectile projectileComponent = bulletInstance.GetComponent<Projectile>();
        projectileComponent.shooterTag = shooterTag;

        bulletInstance.velocity = direction * projectileComponent.speed;

        m_LastShotTime = Time.time;
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
