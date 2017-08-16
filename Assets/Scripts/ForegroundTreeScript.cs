using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundTreeScript : MonoBehaviour
{

    public float easeValue;
    public float speed;
    private float value;

    public Rigidbody2D Cat;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate ()
    {

        if (Cat.velocity.magnitude <= 0 || Timer.time >= 30f)
        {
            speed = 0;
        }
        else if (Cat.velocity.magnitude > 0 && Cat.velocity.magnitude <= 5 && Timer.time >= 3f)
        {
            if (speed <= 3f)
            {
                speed = speed + 0.01f;
            }
        }
        else if (Cat.velocity.magnitude > 5f && Cat.velocity.magnitude <= 10 && Timer.time >= 3f)
        {
            if (speed <= 5f)
            {
                speed = speed + 0.01f;
            }
        }
        else if (Cat.velocity.magnitude > 10f && Cat.velocity.magnitude < 20f && Timer.time >= 3f)
        {
            if (speed <= 7f)
            {
                speed = speed + 0.01f;
            }
        }
        if (Timer.time < 3f)
        {
            easeValue = easeValue * 0.9997f;
        }

        if (Timer.time >= 0)
        {
            Vector2 offset = new Vector2(speed * easeValue * (30f - Timer.time), 0);
            GetComponent<Renderer>().material.mainTextureOffset = offset;
        }

    }
}
