using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehaviour1 : MonoBehaviour
{
    //For the directional ray
    private LineRenderer lineRenderer;
    private Vector2 direction;
    private RaycastHit2D raycast;

    //touch controls
    public static bool touchActive = true;
    private bool setEnded;
    private bool alternate;
    private bool slow;

    //Time controls
    public float time;
    private float powerUpTime;

    //Force and velocity controls
    private float force = 8000f;
    private float thresholdVelocity = 950f;
    private Vector2 velocity;

    //Rotation controls
    private Quaternion initialRotation;

    //Power Up controls
    private bool newPowerTouch;
    private float touchTime;
    private bool longPressDetected;

    //Reference to rigidbody cat
    private Rigidbody2D rb;

    //Animators
    private Animator anim;
    private int jumpHashCode = Animator.StringToHash("jumped");
    private int rolledHashCode = Animator.StringToHash("isRolled");
    private int jumpStartHashCode = Animator.StringToHash("jumpStart");
    private int jumpAnimation1HashCode = Animator.StringToHash("Base Layer.Jump Animation1");
    private bool alternateAnim = false;

    Touch touch;
    Vector2 touchPos;

    //Collider
    public static CircleCollider2D collider2DCat;

    void Start()
    {
        alternate = true;
        rb = GetComponent<Rigidbody2D>();
        touchActive = true;
        time = 10f;
        setEnded = false;
        slow = false;
        powerUpTime = 10f;
        newPowerTouch = false;
        longPressDetected = false;
        anim = GetComponent<Animator>();
        collider2DCat = GetComponent<CircleCollider2D>();
        collider2DCat.sharedMaterial.bounciness = 1;
        initialRotation = rb.transform.rotation;
        rb.gravityScale = 0.059f;
        
    }

    void Update()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && touchActive)
        {
            touch = Input.GetTouch(0);
            touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            anim.Play("Base.JumpStart");
            alternateAnim = true;
            anim.Update(Time.smoothDeltaTime);
            anim.Update(Time.smoothDeltaTime);
            touchActive = false;
        }

        if (alternateAnim && !touchActive)
        {
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.2)
            {
                Debug.Log("here");
                rb.AddForce((touchPos - new Vector2(transform.position.x, transform.position.y)).normalized * 5000f);
            }
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.2)
            {
                Debug.Log("here");
                anim.Play("Base.JumpAnimation");
                rb.AddTorque(-10f);
                velocity = rb.velocity;
                alternateAnim = false;
                collider2DCat.radius = 1.8f;
            }
        }

        if (Timer.hasEnded)
        {
            Debug.Log("here");
            rb.transform.rotation = initialRotation;
            anim.Play("Base.tuckStanding");
            collider2DCat.radius = 3.77f;
        }

        //Power up
        if (Input.touchCount == 1 && !touchActive)
        {
            Debug.Log("here");
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchTime = Time.time;
            }
            else if (touch.phase == TouchPhase.Stationary && Timer.time > 5f && !PowerUp.active)
            {
                newPowerTouch = true;
                if (Time.time - touchTime > 1)
                {
                    Handheld.Vibrate();
                    PowerUp.active = true;
                    Timer.isPausedStarted = true;
                }
            }
            else if (touch.phase == TouchPhase.Ended && !PowerUp.active && Timer.time > 3f)
            {
                newPowerTouch = false;
                //Debug.Log("Ended" + Time.time);
                if (alternate && !Timer.hasEnded)
                {
                    slow = true;
                    alternate = false;
                }
                else if (!Timer.hasEnded)
                {
                    slow = false;
                    touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    rb.AddForce((touchPos - new Vector2(transform.position.x, transform.position.y)).normalized * force);
                    alternate = true;
                }
            }
        }

        //Limit velocity
        if (rb.velocity.sqrMagnitude > thresholdVelocity)
        {
            Debug.Log("here");
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
            rb.velocity = rb.velocity * 0.9f;
        }
    }

}
