﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehaviour1 : MonoBehaviour
{
    //For the directional ray
    public Transform raySpawnPointer;
    public Transform hitPoint;
    private LineRenderer lineRenderer;
    private Vector2 direction;
    private RaycastHit2D raycast;

    //touch controls
    public static bool touchActive;
    private bool setEnded;
    private bool alternate;
    private bool slow;

    //Time controls
    public float time;
    private float powerUpTime;

    //Force and velocity controls
    private float force = 9000f;
    private float thresholdVelocity = 950f;
    private Vector2 velocity;

    //Rotation controls
    private Quaternion initialRotation;

    //Power Up controls
    private bool newPowerTouch;
    private float touchTime;
    private bool longPressDetected;
    private float someValue = 0.5f;

    //Reference to rigidbody cat
    private Rigidbody2D rb;

    //Animators
    private Animator anim;
    private int jumpHashCode = Animator.StringToHash("jumped");
    private int rolledHashCode = Animator.StringToHash("isRolled");
    private int jumpStartHashCode = Animator.StringToHash("jumpStart");
    private int jumpAnimation1HashCode = Animator.StringToHash("Base Layer.Jump Animation1");
    private bool alternateAnim = false;

    //Collider
    private CircleCollider2D collider2D;

    void Start()
    {
        alternate = true;
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.enabled = false;
        touchActive = true;
        time = 10f;
        setEnded = false;
        direction = hitPoint.position - raySpawnPointer.position;
        raycast = Physics2D.Raycast(raySpawnPointer.position, direction, 5f);
        slow = false;
        powerUpTime = 10f;
        newPowerTouch = false;
        longPressDetected = false;
        anim = GetComponent<Animator>();
        collider2D = GetComponent<CircleCollider2D>();
        collider2D.sharedMaterial.bounciness = 1;
        initialRotation = rb.transform.rotation;
        
    }

    void Update()
    {

        //LineRenderer set up
        direction = hitPoint.position - raySpawnPointer.position;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && touchActive)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchDeltaPosition = touch.deltaPosition;
            raySpawnPointer.RotateAround(transform.position, Vector3.back, touchDeltaPosition.x * 0.5f);
            hitPoint.RotateAround(transform.position, Vector3.back, touchDeltaPosition.x * 0.5f);
            //transform.Rotate(Vector3.back, touchDeltaPosition.x * 0.5f, Space.Self);
            direction = hitPoint.position - raySpawnPointer.position;
            raycast = Physics2D.Raycast(raySpawnPointer.position, direction, 5f);
            if (raycast.point != null && raycast.collider.tag == "environment")
            {
                Vector2 inDirection = Vector2.Reflect(direction, raycast.normal);
                lineRenderer.SetPosition(0, raySpawnPointer.position);
                lineRenderer.SetPosition(1, raycast.point);
                lineRenderer.SetPosition(2, inDirection);
            }
            else
            {
                lineRenderer.SetPosition(0, raySpawnPointer.position);
                lineRenderer.SetPosition(1, raycast.point);
                lineRenderer.SetPosition(2, raycast.point);
            }
            lineRenderer.enabled = true;
            setEnded = true;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && setEnded)
        {
            lineRenderer.enabled = false;
            anim.Play("Base.JumpStart");
            alternateAnim = true;
            anim.Update(Time.smoothDeltaTime);
            anim.Update(Time.smoothDeltaTime);
        }

        if (alternateAnim)
        {
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.1)
            {
                rb.AddForce(direction * 5000f);
                setEnded = false;
                touchActive = false;
            }
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 2)
            {
                anim.Play("Base.JumpAnimation");
                rb.AddTorque(-100f);
                anim.SetBool(rolledHashCode, true);
                velocity = rb.velocity;
                alternateAnim = false;
                collider2D.radius = 1.8f;
            }
        }

        if (Timer.hasEnded)
        {
            rb.transform.rotation = initialRotation;
            anim.Play("Base.tuckStanding");
            collider2D.radius = 3.77f;
        }

        //Power up
        if (Input.touchCount > 0 && !touchActive)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                newPowerTouch = true;
                touchTime = Time.time;
                Debug.Log("touchTime=" + touchTime);
                Debug.Log("Time=" + Time.time);
                if (alternate && !Timer.hasEnded)
                {
                    slow = true;
                    alternate = false;
                }
                else if (!Timer.hasEnded)
                {
                    slow = false;
                    Vector2 touchPos = Camera.FindObjectOfType<Camera>().ScreenToWorldPoint(touch.position);
                    rb.AddForce((touchPos - new Vector2(transform.position.x, transform.position.y)).normalized * force);
                    alternate = true;
                }
            } 
            else if (touch.phase == TouchPhase.Moved && touch.deltaPosition.magnitude < someValue)
            {
                Debug.Log("Here");
                if (newPowerTouch == true && Time.time - touchTime >= 0.3f)
                {
                    Debug.Log("longpress detected");
                    newPowerTouch = false;
                    longPressDetected = true;
                }
            }
        }

        //Limit velocity
        if (rb.velocity.sqrMagnitude > thresholdVelocity)
        {
            float brakeSpeed = rb.velocity.sqrMagnitude - thresholdVelocity;  // calculate the speed decrease

            Vector3 normalisedVelocity = rb.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

            rb.AddForce(-brakeVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    void FixedUpdate()
    {
        if (slow)
        {
            rb.velocity = rb.velocity * 0.95f;
        }
    }

}
