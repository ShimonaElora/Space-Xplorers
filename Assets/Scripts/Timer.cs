using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    Text TimerText;

    float time;

    public Rigidbody2D rb;

    void Start () {
        TimerText = GetComponent<Text>();
        time = 10f;
        TimerText.text = "Time Left: " + Mathf.Round(time).ToString();
        TimerText.alignment = TextAnchor.UpperLeft;
    }

	void Update()
    {
        if (time > 0 && rb.velocity != rb.velocity * 0)
        {
            time -= Time.deltaTime;
            TimerText.text = "Time Left: " + Mathf.Round(time).ToString();
        }
    }


    void FixedUpdate()
    {
        if (time <= 0.5)
        {
            rb.velocity = rb.velocity * 0.96f;
            rb.angularVelocity = rb.angularVelocity * 0.96f;
        }
    }
}
