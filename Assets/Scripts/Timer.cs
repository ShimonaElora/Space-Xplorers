using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    Text TimerText;

    float time;

    public Rigidbody2D rb;

    public static bool hasEnded;

    void Start () {
        TimerText = GetComponent<Text>();
        time = 30f;
        TimerText.text = "Time Left: 30";
        hasEnded = false;
    }

	void Update()
    {
        if (time > 0 && !CatBehaviour1.touchActive)
        {
            time -= Time.deltaTime;
            TimerText.text = "Time Left: " + Mathf.Round(time).ToString();
        }
    }


    void FixedUpdate()
    {
        if (time <= 0.5 && time >= -1)
        {
            hasEnded = true;
            rb.velocity = rb.velocity * 0.96f;
            rb.angularVelocity = rb.angularVelocity * 0.96f;
            rb.gravityScale = 0.3f;
        }
    }
}
