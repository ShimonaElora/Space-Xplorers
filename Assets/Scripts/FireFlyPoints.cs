using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireFlyPoints : MonoBehaviour {

    public static int fireflyPoints = 0;

    Text fireflyPointText;

    // Use this for initialization
    void Start () {
        fireflyPointText = GetComponent<Text>();
        fireflyPointText.text = fireflyPoints.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        fireflyPointText.text = fireflyPoints.ToString();
    }
}
