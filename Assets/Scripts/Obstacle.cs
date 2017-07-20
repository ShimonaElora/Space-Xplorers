using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    CircleCollider2D obstacleField;

	// Use this for initialization
	void Start () {
        obstacleField = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            obstacleField.enabled = false;
        }
    }
}
