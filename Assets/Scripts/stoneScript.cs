using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "environment")
        {
            StartCoroutine(timerFinish());
        }
    }

    IEnumerator timerFinish()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
