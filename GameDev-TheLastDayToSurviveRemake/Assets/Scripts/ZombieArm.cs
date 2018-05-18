using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieArm : MonoBehaviour {
    public int damage = 2;
    public ZombieHitDetector hitDetector;

	
	void Start () {
		
	}
	
	
	void Update () {
		
	}

    private void OnTriggerEnter2D (Collider2D collision) {
        if (hitDetector.isConflicDamage) {
            Player player = collision.gameObject.GetComponent<Player>();
            Fence fence = collision.gameObject.GetComponent<Fence>();

            if (fence) {
                hitDetector.isConflicDamage = false;
                fence.GetHit(damage);
            }
            else if (player) {
                hitDetector.isConflicDamage = false;
                player.GetHit(damage);
            }
        }
    }
}
