using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerControl pc;
    public Vector2 moveDir;
    private Rigidbody2D rb;
    public float jumpForce = 10f;
    public float maxSpeed = 7f;
    public bool isJumping;
    public float JumpDelay = 1f;
    public float distFromGround;
    public int JumpCount = 0;
    public int MaxJumps = 3;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerGameObject = GameObject.FindWithTag("Player"); 
        //GameObject.FindObjectOfType<PlayerControl>().gameObject;
         pc = playerGameObject.GetComponent<PlayerControl>();
        rb.gravityScale = 1.25f;

    }

    // Update is called once per frame
    void Update()

    {
        if (rb.velocity.y == 0)
        {
            JumpCount = 0;
        }
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
        //if player is above enemy, move away from player
        if (pc.getPosition().y > rb.transform.position.y)
        {
            if (pc.getPosition().x > transform.position.x)
            {
                rb.AddForce(new Vector2(-3, 0));
                if(!isJumping && JumpCount<MaxJumps)
                    rb.AddForce(new Vector2(0, jumpForce));
            }
            else
            {
                rb.AddForce(new Vector2(3, 0));
                if (!isJumping && JumpCount < MaxJumps)
                    rb.AddForce(new Vector2(0, jumpForce));
            }
        }

        else  // if player is below enemy, move towards player
        {
            if (pc.getPosition().x > transform.position.x)
            {
                rb.AddForce(new Vector2(3, 0));
                StartCoroutine(handleJumping());
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
    private IEnumerator handleJumping()
    {
        isJumping = true;
        yield return new WaitForSeconds(JumpDelay);
        JumpCount++;
        isJumping = false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
