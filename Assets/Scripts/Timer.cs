using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    Text TimerText;

    public static float time;

    public Rigidbody2D rb;

    public static bool hasEnded;

    public static bool isPaused;
    public static bool isPausedStarted;
    public static bool isPausedStartEnded;

    void Start () {
        TimerText = GetComponent<Text>();
        time = 30f;
        TimerText.text = "Time Left: 30";
        hasEnded = false;
        isPaused = false;
        isPausedStarted = false;
        isPausedStartEnded = false;
    }

	void Update()
    {
        if (time > 0 && !CatBehaviour1.touchActive && !isPaused)
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
            rb.gravityScale = 0.5f;
            CatBehaviour1.collider2DCat.sharedMaterial.bounciness = 0;
        }
    }
}
