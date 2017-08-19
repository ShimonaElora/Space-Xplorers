using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElderScript : MonoBehaviour {

    public GameObject Cat;

    private RaycastHit2D raycast;
    private Vector3 direction;

    private bool active = true;
    private LayerMask layerMask;

	// Use this for initialization
	void Start () {
        Cat = GameObject.FindGameObjectWithTag("Player");
        direction = Cat.transform.position - transform.position;
        layerMask = 2;
	}
	
	// Update is called once per frame
	void Update () {
        direction = Cat.transform.position - transform.position;
        raycast = Physics2D.Raycast(transform.position, direction, 7f);
        Debug.DrawLine(transform.position, raycast.point, Color.blue);
        if (raycast.collider != null && raycast.collider.tag == "Player" && active)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2( direction.x * 1.5f, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            active = false;
        }
        if (collision.collider.gameObject.name == "wallLeft")
        {
            Destroy(gameObject);
        }
    }

}
