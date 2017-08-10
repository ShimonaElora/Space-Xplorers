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
    private float offsetFarLeft;

    private Vector3 velocity1 = new Vector3(1, 1, 0);

    private Camera cameraMain;

    private float offsetX;

    public static bool lerpStarted;
    private float lerpStartTime;
    private float totalLerpTime = 2f;
    private float percentage;

    void Start () {
        offsetHorizontal = transform.position.x - Cat.transform.position.x;
        offsetX = offsetHorizontal / 2;
        offsetVertical = transform.position.y - Cat.transform.position.y;
        offsetFarLeft = transform.position.x - farLeft.position.x;
        cameraMain = GetComponent<Camera>();
        lerpStarted = false;
    }

    void FixedUpdate () {

        Vector3 newPosition = transform.position;

        if (offsetHorizontal + offsetX > offsetHorizontal / 2)
        {
            offsetX -= offsetX * 0.1f;
        } else if (offsetHorizontal + offsetX < offsetHorizontal / 2)
        {
            offsetX -= offsetX * 0.1f;
        }
        if (offsetX == offsetHorizontal / 2)
        {
            newPosition.x = Cat.transform.position.x + offsetX;
        } else
        {
            newPosition.x = Cat.transform.position.x - offsetX;
        }
        
        float distance = Cat.transform.position.y + offsetVertical;


        //Setting wallleft position
        if (newPosition.x > transform.position.x)
        {
            float positionLeft = farLeft.position.x + (newPosition.x - transform.position.x) - offsetHorizontal / 2;
            positionLeft = Mathf.Clamp(farLeft.position.x, transform.position.x - (offsetHorizontal * 1.7f), transform.position.x);
            farLeft.position = new Vector2(positionLeft, farLeft.position.y);
        }
        newPosition.x = Mathf.Clamp(newPosition.x, farLeft.position.x + offsetFarLeft, farRight.position.x);


        if (Timer.isPaused)
        {
            if (Timer.isPausedStarted)
            {
                if (cameraMain.orthographicSize > 5.1f && cameraMain.orthographicSize <= 6.7f)
                {
                    float cameraSize = cameraMain.orthographicSize * 0.99f;
                    cameraSize = Mathf.Clamp(cameraSize, 5f, 6.6f);
                    cameraMain.orthographicSize = cameraSize;
                    newPosition.y = transform.position.y - 0.06f;
                    newPosition.y = Mathf.Clamp(newPosition.y, -1.227f, 1.5f);
                    transform.position = Vector3.Lerp(transform.position, newPosition, 0.9f);
                }
                if (cameraMain.orthographicSize <= 5.1f)
                {
                    Timer.isPausedStartEnded = true;
                }
                if (PowerUp.isEndStarted)
                {
                    offsetX = Cat.transform.position.x - transform.position.x;
                    newPosition.x = Cat.transform.position.x + offsetX;
                    PowerUp.isEndStartEnded = true;
                }
            }
            
        } else
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, 0.95f);
            if ((distance >= offsetVertical * 1.7f || Cat.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > 500f) && cameraMain.orthographicSize <= 6.6f)
            {
                float cameraSize = cameraMain.orthographicSize * 1.01f;
                cameraSize = Mathf.Clamp(cameraSize, 5f, 6.6f);
                cameraMain.orthographicSize = cameraSize;
                newPosition.y = transform.position.y + 0.06f;
                newPosition.y = Mathf.Clamp(newPosition.y, transform.position.y, 1.43f);
                transform.position = Vector3.Lerp(transform.position, newPosition, 0.9f);

            } else if (
                (distance < offsetVertical * 1.7f || Cat.GetComponent<Rigidbody2D>().velocity.sqrMagnitude < 300f)
                && cameraMain.orthographicSize <= 6.7f && cameraMain.orthographicSize >= 5f)
            {
                cameraMain.orthographicSize = cameraMain.orthographicSize * 0.99f;
                newPosition.y = transform.position.y - 0.06f;
                newPosition.y = Mathf.Clamp(newPosition.y, -1.227f, transform.position.y);
                transform.position = Vector3.Lerp(transform.position, newPosition, 0.9f);
            }
        }
        

    }
}
