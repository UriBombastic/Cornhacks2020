using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 moveDir;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(moveDir);
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.GetComponent<PlayerControl>())
            if (col.transform.position.y > transform.position.y)
                Die();
        
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
