using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMinionScript : MonoBehaviour {

    public Transform catTransform;

    private RaycastHit2D raycast;

    private Vector3 direction;

    private Vector2 velocityEnemy;

    public Transform stoneSpawnTransfrom;

    public GameObject stone;
    public GameObject minion;

    private bool canShoot;

    // Use this for initialization
    void Start() {
        catTransform = GameObject.FindGameObjectWithTag("Player").transform;
        direction = catTransform.position - stoneSpawnTransfrom.position;
        velocityEnemy = new Vector2(1f, 0);
        canShoot = true;
    }

    // Update is called once per frame
    void Update() {
        velocityEnemy = new Vector2(1f, 0);
        direction = catTransform.position - stoneSpawnTransfrom.position;
        raycast = Physics2D.Raycast(stoneSpawnTransfrom.position, direction, 10f);
        Debug.DrawLine(stoneSpawnTransfrom.position, raycast.point, Color.blue);
        if (raycast.collider != null && raycast.collider.tag == "Player" && canShoot && Timer.time < 30 && Timer.time >= 0)
        {
            canShoot = false;
            StartCoroutine(throwStone ());
        }
        GetComponent<Rigidbody2D>().velocity = velocityEnemy;
    }

    IEnumerator throwStone () {
        yield return new WaitForSeconds(1);
        Instantiate(stone, stoneSpawnTransfrom.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = direction.normalized * 15f;
        canShoot = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Destroy(minion);
        }
        if (collision.collider.gameObject.name == "wallLeft")
        {
            Destroy(minion);
        }
    }
}
