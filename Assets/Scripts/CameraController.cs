using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Cat;
    public Transform farLeft;
    public Transform farRight;

    private Vector2 velocity;
    public float smoothTimeX;

    private float offset;

    void Start () {
        offset = transform.position.x - Cat.transform.position.x;
    }

    void FixedUpdate () {

        Vector3 newPosition = transform.position;

        newPosition.x = Cat.transform.position.x + offset;
        newPosition.x = Mathf.Clamp(newPosition.x, farLeft.position.x + offset, farRight.position.x - offset);

        transform.position = newPosition;
    }
}
