using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float MoveForce;
    public float jumpForce = 10f;
    public float maxSpeed = 10f;
    public float MaxHealth;
    public float Health;
    public int MaxJumps;
    private Rigidbody2D rb;
    public bool isJumping;
    public float JumpDelay = 0.5f;
    public float distFromGround;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;   
        
    }

    // Update is called once per frame
    void Update()
    {
        distFromGround = transform.localPosition.y-((float)-4.17);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h, v);
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
        if (Health <= 0)
            Die();
    }

    void Move(float h, float v)
    {
        if (h != 0 || v != 0)
        {
            Vector2 moveDirection = new Vector2(h, (v > 0 ? 0 : v)) * MoveForce;
            rb.AddForce(moveDirection);
            //flips sprite according to velocity
            if (h < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
            else if (h > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
            
            if (v > 0 && !isJumping)
            {
                rb.AddForce(new Vector2(0, jumpForce));
                StartCoroutine(handleJumping());
            }
        }
        if (h == 0 && transform.position.y <= -4)
        {
            rb.velocity = new Vector2((float)0.0, rb.velocity.y);
        }
    }

    private IEnumerator handleJumping()
    {
        isJumping = true;
        yield return new WaitForSeconds(JumpDelay);
        isJumping = false;
    }

    private bool OnGround()
    {
        return Physics.Raycast(transform.position,-Vector2.up,distFromGround+(float).1);
    }

    public void TakeDamage()
    {
        Health--;
    }

    public void Die()
    {
        GameMaster.Instance().HandleDeath();
    }
}
