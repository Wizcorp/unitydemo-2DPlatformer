using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFence : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Flip();
        }
            
    }
}
