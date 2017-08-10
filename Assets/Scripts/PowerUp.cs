using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public static bool active;

    public static int numberOfTaps = 1;

    public static bool isEndStarted;
    public static bool isEndStartEnded;

    private bool decreaseTime;

    float time;

    void Start()
    {
        active = false;
        time = 4f;
        decreaseTime = true;
        isEndStarted = false;
        isEndStartEnded = false;
    }

    void Update()
    {
        if (active)
        {
            Timer.isPaused = true;
            if (Timer.isPausedStartEnded)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector2 touchPos = Camera.FindObjectOfType<Camera>().ScreenToWorldPoint(touch.position);
                    transform.position = new Vector3(touchPos.x, touchPos.y, transform.position.z);
                    decreaseTime = false;
                    numberOfTaps--;
                    if (numberOfTaps <= 0)
                    {
                        active = false;
                        isEndStarted = true;
                        CameraController.lerpStarted = true;
                    }
                }
                if (decreaseTime)
                {
                    time -= Time.deltaTime;
                }
                
            }
        }
        if (time <= 0 || (!active && isEndStartEnded))
        {
            Timer.isPaused = false;
            time = 4f;
            active = false;
            decreaseTime = true;
            Timer.isPausedStarted = false;
            Timer.isPausedStartEnded = false;
            isEndStartEnded = false;
            isEndStarted = false;
            numberOfTaps = 1;
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
        
    }
}
