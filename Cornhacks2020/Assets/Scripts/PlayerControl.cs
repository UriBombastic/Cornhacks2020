using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float MoveForce;
    public float jumpForce = 10f;
    public float maxSpeed = 10f;
    public float MaxHealth = 5;
    public float Health;
    public int MaxJumps;
    private Rigidbody2D rb;
    public bool isJumping;
    public bool isDamaged;
    public float JumpDelay = 0.5f;
    public float DamageDelay = 2.0f;
    public float distFromGround;
    public int JumpCount = 0;
    SpriteRenderer sr;
    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        MaxJumps = 3;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = Sprite1;

    }

    // Update is called once per frame
    void Update()
    {
        distFromGround = transform.localPosition.y - ((float)-4.17);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h, v);
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
        if (Health <= 0)
            Die();
        if (Health == 2)
        {
            sr.sprite = Sprite2;
        }
        else if (Health == 1)
        {
            sr.sprite = Sprite2;

        }
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

            if (v > 0 && !isJumping && (JumpCount < MaxJumps))
            {
                JumpCount++;
                rb.AddForce(new Vector2(0, jumpForce));
                StartCoroutine(handleJumping());
            }
        }
        if (h == 0 && transform.position.y <= -4)
        {
            rb.velocity = new Vector2((float)0.0, rb.velocity.y);
        }
        if (v == 0 && rb.velocity.y == 0)
        {
            JumpCount = 0;
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
        return Physics.Raycast(transform.position, -Vector2.up, distFromGround + (float).1);
    }
    public Vector2 getPosition()
    {
        return transform.position;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.GetComponent<Enemy>() && isDamaged == false)
        {
            if (col.transform.position.y > transform.position.y)
            {
                StartCoroutine(handleDamage());
                Health--;
                 
            }
        }
        

    }
    private IEnumerator handleDamage()
    {
        isDamaged = true;
        yield return new WaitForSeconds(DamageDelay);
        isDamaged = false;
    }

    public void Die()
    {
        GameMaster.Instance().HandleDeath();
    }
}
