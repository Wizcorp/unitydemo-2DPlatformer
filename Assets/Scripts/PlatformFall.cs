using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour {

    public float fallDelay = 1f;


    private Rigidbody2D rb2d;

    void Awake()
    {
        gameObject.AddComponent<Rigidbody2D>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.isKinematic = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("ground"))
        {
            Invoke("Fall", fallDelay);
        }
    }

    void Fall()
    {
        rb2d.isKinematic = false;
    }

}
