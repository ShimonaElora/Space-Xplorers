using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnerScript : MonoBehaviour
{

    public GameObject[] enemies;

    public int minWait, maxWait, startWait;

    public float positionX;

    private int spawnWait;
    private bool startCoroutine;
    private GameObject enemy;

    // Use this for initialization
    void Start()
    {
        startCoroutine = true;
    }

    // Update is called once per frame
    void Update()
    {
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

        while (Timer.time >= 5)
        {
            enemy = enemies[Random.Range(0, 4)];

            Vector3 spawnPosition = new Vector3(transform.position.x + positionX, enemy.transform.position.y, enemy.transform.position.z);

            Instantiate(enemy, spawnPosition, transform.rotation);

            yield return new WaitForSeconds(spawnWait);
        }
    }

}
