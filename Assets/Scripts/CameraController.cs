using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Cat;
    public Transform farLeft;
    public Transform farRight;

    private Vector2 velocity;
    public float smoothTimeX;

    private float offsetHorizontal;
    private float offsetVertical;

    void Start () {
        offsetHorizontal = transform.position.x - Cat.transform.position.x;
        offsetVertical = transform.position.y - Cat.transform.position.y;
    }

    void FixedUpdate () {

        Vector3 newPosition = transform.position;

        newPosition.x = Cat.transform.position.x + offsetHorizontal/2;
        newPosition.y = Cat.transform.position.y + (offsetVertical/2);
        if (newPosition.x > transform.position.x)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, farLeft.position.x + offsetHorizontal, farRight.position.x - offsetHorizontal);
        }
        else
        {
            newPosition.x = transform.position.x;
        }
        newPosition.y = Mathf.Clamp(newPosition.y, -4.651005f - offsetVertical, 8.748996f);

        transform.position = newPosition;
    }
}
