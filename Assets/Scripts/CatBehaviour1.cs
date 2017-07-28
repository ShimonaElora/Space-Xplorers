using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehaviour1 : MonoBehaviour
{

    public int speed;
    public Transform arrowPointer;
    public Transform hitPoint;
    LineRenderer lineRenderer;
    bool touchActive;
    public float time;
    bool obstacleField;
    bool setEnded;
    Vector2 direction;
    Vector2 offset;
    Vector2 endPoint;
    bool alternate;
    bool slow;
    bool wallTop;
    float v1;

    Vector2 velocity;
    RaycastHit2D raycast;

    Rigidbody2D rb;

    void Start()
    {
        wallTop = true;
        alternate = true;
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.enabled = false;
        touchActive = true;
        time = 10f;
        obstacleField = false;
        setEnded = false;
        direction = hitPoint.position - transform.position;
        raycast = Physics2D.Raycast(arrowPointer.position, direction);
        offset = new Vector2(hitPoint.position.x, hitPoint.position.y) - raycast.point;
        slow = false;
    }

    void Update()
    {
        direction = hitPoint.position - transform.position;
        raycast = Physics2D.Raycast(arrowPointer.position, direction, 5f);
        if (raycast.point != null && raycast.collider.tag == "environment")
        {
            Vector2 inDirection = Vector2.Reflect(direction, raycast.normal);
            lineRenderer.SetPosition(0, arrowPointer.position);
            lineRenderer.SetPosition(1, raycast.point);
            lineRenderer.SetPosition(2, inDirection);
        }
        else
        {
            lineRenderer.SetPosition(0, arrowPointer.position);
            lineRenderer.SetPosition(1, raycast.point);
            lineRenderer.SetPosition(2, raycast.point);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && touchActive)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Rotate(Vector3.back, touchDeltaPosition.x * 0.5f, Space.Self);
            lineRenderer.enabled = true;
            setEnded = true;

        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && setEnded)
        {
            lineRenderer.enabled = false;
            rb.AddForce(direction * 5000f);
            velocity = rb.velocity;
            setEnded = false;
            touchActive = false;
        }
        if (alternate)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !touchActive)
            {
                slow = true;
                alternate = false;
            }
        }
        else
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !touchActive)
            {
                slow = false;
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = Camera.FindObjectOfType<Camera>().ScreenToWorldPoint(touch.position);
                rb.AddForce((touchPos - new Vector2(transform.position.x, transform.position.y)).normalized * 20000f);
                alternate = true;
            }

        }

        if (rb.velocity.magnitude < 0.1f)
        {
            obstacleField = false;
        }
        else if (rb.velocity.sqrMagnitude > 950)
        {
            float brakeSpeed = rb.velocity.sqrMagnitude - 950;  // calculate the speed decrease

            Vector3 normalisedVelocity = rb.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

            rb.AddForce(-brakeVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Flipper" && rb.velocity.magnitude <= 650f)
        {
            rb.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            obstacleField = true;
            lineRenderer.enabled = true;
        }
        if (collision.gameObject.tag == "wallTopOuter")
        {
            if (wallTop)
            {
                v1 = rb.velocity.sqrMagnitude;
                rb.AddForce(-rb.velocity.normalized * v1 * 50f);
                wallTop = false;
            }
            else
            {
                if (v1 <= 1000f)
                {
                    rb.AddForce(rb.velocity.normalized * v1 * 40f);  
                }
                wallTop = true;
            }
        } 
    }

    void FixedUpdate()
    {
        if (obstacleField && rb.velocity.magnitude >= 0.1f)
        {
            rb.velocity = rb.velocity * 0.73f;
            rb.angularVelocity = rb.angularVelocity * 0.73f;
        }

        if (slow)
        {
            rb.velocity = rb.velocity * 0.95f;
        }
    }
}
