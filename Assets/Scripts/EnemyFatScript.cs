using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFatScript : MonoBehaviour {

    public Transform Cat;
    public float timeTillHit = 1f;
    public GameObject gameObjectEnemy;

    Vector3 direction;

    RaycastHit2D raycast;

    Rigidbody2D rb;

    bool active;

    bool groundRunning = false;

    // Use this for initialization
    void Start() {
        direction = Cat.transform.position - transform.position;
        rb = GetComponent<Rigidbody2D>();
        active = true;
    }

    // Update is called once per frame
    void Update() {
        if (active)
        {
            direction = Cat.transform.position - transform.position;
            raycast = Physics2D.Raycast(transform.position, direction, 4f);
            Debug.DrawLine(transform.position, raycast.point, Color.blue);
            if (raycast != null && raycast.collider.tag == "Player")
            {
                Throw();
                active = false;
            }
        }
    }

    void Throw() {
        if (transform.position.y < Cat.position.y)
        {
            rb.velocity = new Vector2((Cat.position.x - transform.position.x) * 1.4f, (Cat.position.y - transform.position.y) * 9f);
        } else
        {
            rb.velocity = new Vector2((Cat.position.x - transform.position.x) * 1.4f, transform.position.y * 2f);
        }
        
        rb.gravityScale = 5f;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "environment")
        {
            rb.gravityScale = 1f;
            rb.velocity = Vector2.zero;
            groundRunning = true;
        }
        if (collision.collider.tag == "platform")
        {
            rb.velocity = new Vector2(-5f, 2f);
        }
        if (collision.collider.gameObject.name == "wallLeft")
        {
            Destroy(gameObjectEnemy);
        }
    }

    private void FixedUpdate()
    {
        if (groundRunning)
        {
            rb.velocity = new Vector2(-5f, 0f);
        }
    }

}
