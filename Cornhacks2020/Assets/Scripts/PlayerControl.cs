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
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h, v);
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
    }

    void Move(float h, float v)
    {
        Vector2 moveDirection = new Vector2(h, (v>0?0 : v)) * MoveForce;
        rb.AddForce(moveDirection);
        if(v > 0 && !isJumping)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            StartCoroutine(handleJumping());
        }
    }

    private IEnumerator handleJumping()
    {
        isJumping = true;
        yield return new WaitForSeconds(JumpDelay);
        isJumping = false;
    }

}
