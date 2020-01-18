using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Vector3 MoveDirection;
    public float maxSpeed = 5f;
    private Rigidbody2D rb;
    public float SpawnTime = 1.5f;
    public bool canSpawn = true;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(MoveDirection);
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
        if (canSpawn) StartCoroutine(SpawnEnemy());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MoveDirection *= -1;
    }

    public IEnumerator SpawnEnemy()
    {
        canSpawn = false;
        Instantiate(enemy, transform);
        yield return new WaitForSeconds(SpawnTime);
        canSpawn = true;
    }
}
