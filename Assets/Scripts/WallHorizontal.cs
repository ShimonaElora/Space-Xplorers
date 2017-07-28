using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHorizontal : MonoBehaviour
{

    public Transform spawnPoint;
    public GameObject wall;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wallRight")
        {
            GameObject wallClone = (GameObject)Instantiate(wall, spawnPoint.position, wall.transform.rotation);
        }
    }
}
