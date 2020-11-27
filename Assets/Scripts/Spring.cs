using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    bool bounce = false;
    float bounceAmount = 10;
    Transform player;
 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.transform;
            bounce = true;
        }
    }

    void Update()
    {
        if (bounce)
        {
            Rigidbody2D playerRigidbodyComponent = player.GetComponent<Rigidbody2D>();
            playerRigidbodyComponent.AddForce(Vector2.up * bounceAmount, ForceMode2D.Impulse);
            bounce = false;
        }
    }
}
