using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifleBullet : MonoBehaviour {
    public int damage = 40;
    public float speed = 50;
    public float lifeTime = 5f;

    private float spawnTime;

	// Use this for initialization
	void Start () {
        spawnTime = Time.time;

    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if(Time.time - spawnTime >= lifeTime) {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D (Collider2D collision) {
        Zombie zombie = collision.gameObject.GetComponent<Zombie>();
        if (zombie) {
            zombie.GetHit(damage);
            Destroy(gameObject);
        }
    }
}
