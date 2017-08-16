using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyRespawn : MonoBehaviour {

    public GameObject theObject;
    public float maxPosY;
    public float minPosY;
    public float minWait;
    public float maxWait;
    public float positionX;
    public int startWait;
    private float spawnWait;
    float max = 20f;
    private bool startCoroutine;

	// Use this for initialization
	void Start () {
        startCoroutine = true;
    }
	
	// Update is called once per frame
	void Update () {
        spawnWait = Random.Range(minWait, maxWait);
        if (startCoroutine && Timer.time < 30)
        {
            StartCoroutine(spawn());
        }
	}

    IEnumerator spawn()
    {
        startCoroutine = false;
        yield return new WaitForSeconds(startWait);

        while(Timer.time > 0)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x + positionX, Random.Range(-maxPosY, maxPosY), transform.position.z);

            Instantiate(theObject, spawnPosition, transform.rotation);

            yield return new WaitForSeconds(spawnWait);
        }
    }
 
}
