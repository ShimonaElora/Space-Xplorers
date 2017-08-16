using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirflyScript : MonoBehaviour {

    public float horizontalVelocity;
    private float maxHorizontalDistance;
    private float minHorizontalDistance;
    public float verticalVelocity;
    public float amplitude;
    private float startPosY;

    public GameObject gameObjectFireFly;

    private Vector3 tempPosition;
    private bool decreaseRange;

    // Use this for initialization
    void Start () {
        decreaseRange = false;
        startPosY = transform.position.y;
        tempPosition = transform.position;
        maxHorizontalDistance = transform.position.x + 0.5f;
        minHorizontalDistance = transform.position.x - 0.5f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        tempPosition.x += horizontalVelocity;
        if (tempPosition.x >= maxHorizontalDistance || tempPosition.x <= minHorizontalDistance)
        {
            horizontalVelocity = -horizontalVelocity;
        }
        tempPosition.y =  startPosY + Mathf.Sin(Time.realtimeSinceStartup * verticalVelocity) * amplitude;
        transform.position = tempPosition;
        if (decreaseRange)
        {
            GetComponent<Light>().range = GetComponent<Light>().range * 0.95f;
            if (GetComponent<Light>().range <= 0.1f)
            {
                GetComponent<Light>().range = 0f;
                Destroy(gameObjectFireFly);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "Player")
        {
            GetComponent<Renderer>().enabled = false;
            decreaseRange = true;
            FireFlyPoints.fireflyPoints++;
        }
    }

}
