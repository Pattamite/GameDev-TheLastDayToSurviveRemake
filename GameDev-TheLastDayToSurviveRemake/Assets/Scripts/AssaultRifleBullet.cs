using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifleBullet : MonoBehaviour {
    public float damage = 40;
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
}
