using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerControl pc;
    public Vector2 moveDir;
    private Rigidbody2D rb;
    public float jumpForce = 1f;
    public float maxSpeed = 1f;
    public bool isJumping;
    public float JumpDelay = 0.5f;
    public float distFromGround;
    public int JumpCount = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerGameObject = GameObject.FindWithTag("Player"); 
        //GameObject.FindObjectOfType<PlayerControl>().gameObject;
         pc = playerGameObject.GetComponent<PlayerControl>();
        rb.gravityScale = 1;

    }

    // Update is called once per frame
    void Update()

    {
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
        //if player is above enemy, move away from player
        if (pc.getPosition().y > rb.transform.position.y)
        {
            if (pc.getPosition().x > transform.position.x)
            {
                rb.AddForce(new Vector2(3, 0));
            }
            else
            {
                rb.AddForce(new Vector2(-3, 0));
            }
        }
        else  // if player is below enemy, move towards player
        {
            if (pc.getPosition().x > transform.position.x)
            {
                rb.AddForce(new Vector2(3, 0));
            }
            else
            {
                rb.AddForce(new Vector2(-3, 0));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.GetComponent<PlayerControl>())
            if (col.transform.position.y > transform.position.y)
                Die();

    }

    public void Die()
    {
        Destroy(gameObject);
        GameMaster.Instance().HandleKills();
    }

}
