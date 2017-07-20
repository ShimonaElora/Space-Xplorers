using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCenter : MonoBehaviour {

    private int restoreRotation;
    Quaternion originalRotation;
    public Transform markerDown;
    public Transform markerUp;
    //float rotateSpeed = 0.4f;
    float reverse;
    //private Quaternion finalRotationPositive;
    //private Quaternion finalRotationNegative;

    void Start () {
        restoreRotation = 0;
        originalRotation = transform.rotation;
        //finalRotationPositive = Quaternion.Euler(new Vector3(transform.rotation.x + 0, transform.rotation.y + 0, transform.rotation.z + 180f));
        //finalRotationNegative = Quaternion.Euler(new Vector3(transform.rotation.x + 0, transform.rotation.y + 0, transform.rotation.z + 360f));
        reverse = 7f;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).phase != TouchPhase.Moved) {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x <= Screen.width / 2) {
                restoreRotation = 1;
                reverse = 4f;
            }
            else if (touch.position.x > Screen.width / 2) {
                restoreRotation = 2;
                reverse = 4f;
            }
        }

        if ( reverse <= 1.2f )
        {
            restoreRotation = 0;
            reverse = 4f;
        }
    }


    void FixedUpdate () {

        
        if (restoreRotation == 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.deltaTime * 3f);
        } else if (restoreRotation == 1 && reverse > 0)
        {
            float angle = Quaternion.Angle(transform.rotation, markerDown.rotation);
            if (angle <= 180f)
            {
                transform.Rotate(Vector3.forward, angle * 0.4f);
                reverse -= Time.deltaTime * 30f;
            }
        } else if ( restoreRotation == 2 && reverse > 0)
        {
            float angle = Quaternion.Angle(transform.rotation, markerUp.rotation);
            if (angle <= 180f)
            {
                transform.Rotate(Vector3.back, angle * 0.4f);
                reverse -= Time.deltaTime * 30f;
            }
        }

    }
}
