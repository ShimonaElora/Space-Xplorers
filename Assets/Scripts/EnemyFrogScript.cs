using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrogScript : MonoBehaviour {

    public GameObject Cat;
    public Transform tongueSpawnPoint;
    public Transform tongueEndPoint;

    private bool active = true;
    private RaycastHit2D raycast;
    private Vector3 direction;

    private LineRenderer lineRenderer;

    private bool startRetreat = false;

	// Use this for initialization
	void Start () {
        Cat = GameObject.FindGameObjectWithTag("Player");
        direction = Cat.transform.position - transform.position;
        active = true;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.SetPosition(0, tongueSpawnPoint.position);
        GetComponent<SpringJoint2D>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        direction = Cat.transform.position - transform.position;
        raycast = Physics2D.Raycast(transform.position, direction, 4f);
        Debug.DrawLine(tongueSpawnPoint.position, raycast.point, Color.blue);
        if (raycast.collider != null && raycast.collider.tag == "Player" && active)
        {   
            tongueEndPoint.position = Vector2.MoveTowards(tongueEndPoint.position, Cat.transform.position, 10f * Time.deltaTime);
            lineRenderer.SetPosition(1, tongueEndPoint.position);
            lineRenderer.enabled = true;
        } else
        {
            tongueEndPoint.position = Vector2.MoveTowards(tongueEndPoint.position, tongueSpawnPoint.position, 5f * Time.deltaTime);
            lineRenderer.SetPosition(1, tongueEndPoint.position);
            lineRenderer.enabled = true;
        }
        if (startRetreat || (raycast.collider != null && raycast.collider.tag != "Player"))
        {
            tongueEndPoint.position = Vector2.MoveTowards(tongueEndPoint.position, tongueSpawnPoint.position, 5f * Time.deltaTime);
            lineRenderer.SetPosition(1, tongueEndPoint.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Collider2D>().tag == "Player")
        {
            GetComponent<SpringJoint2D>().enabled = true;
            StartCoroutine(springDisabler());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "wallLeft")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator springDisabler()
    {
        yield return new WaitForSeconds(1);
        startRetreat = true;
        GetComponent<SpringJoint2D>().enabled = false;
        active = false;
    }
}
