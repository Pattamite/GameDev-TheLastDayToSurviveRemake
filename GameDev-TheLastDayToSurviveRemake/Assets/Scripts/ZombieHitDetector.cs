using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitDetector : MonoBehaviour {
    public Zombie zombie;

    public bool isConflicDamage;

    private Animator animator;
	
	void Start () {
        isConflicDamage = false;
        animator = GetComponent<Animator>();
    }
	
	
	void Update () {
		
	}

    public void NowConflicDamage () {
        isConflicDamage = true;
    }

    public void NowNotConflicDamage () {
        isConflicDamage = false;
    }

    private void DestroyZombie () {
        zombie.Kill();
    }

    private void OnTriggerStay2D (Collider2D collision) {
        Player player = collision.gameObject.GetComponent<Player>();
        Fence fence = collision.gameObject.GetComponent<Fence>();

        if(player | fence) {
            animator.SetBool("IsAttacking", true);
        }
    }

    private void OnTriggerExit2D (Collider2D collision) {
        Player player = collision.gameObject.GetComponent<Player>();
        Fence fence = collision.gameObject.GetComponent<Fence>();

        if (player | fence) {
            animator.SetBool("IsAttacking", false);
        }
    }
}
