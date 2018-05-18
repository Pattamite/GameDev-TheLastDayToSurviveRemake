using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {
    public Animator animator;
    public GameObject zombieSprite;
    public float speed = 1.5f;
    public int maxHealth = 100;

    private Player Player;
    private bool isDead;
    private int currentHealth;

	void Start () {
        isDead = false;
        currentHealth = maxHealth;
        Player = GameObject.FindObjectOfType<Player>();

    }
	
	void Update () {
        if(!isDead) UpdateRotationAndPosition();
	}

    private void UpdateRotationAndPosition () {
        Vector3 playerPos = Player.transform.position;
        Vector3 currentPosition = transform.position;
        Vector3 currentRotation = zombieSprite.transform.eulerAngles;

        float deltaX = playerPos.x - currentPosition.x;
        float deltaY = playerPos.y - currentPosition.y;

        float rotationAngle = Mathf.Atan2(deltaY, deltaX) / Mathf.PI * 180f;
        zombieSprite.transform.eulerAngles = new Vector3(currentRotation.x, currentRotation.y, rotationAngle);

        Vector3 movement = new Vector3(deltaX, deltaY, 0);
        transform.Translate(movement.normalized * speed * Time.deltaTime);
    }


    public void GetHit(int damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) Dead();
    }

    private void Dead () {
        isDead = true;
        animator.SetBool("IsDead", true);
    }

    public void Kill () {
        Destroy(gameObject);
    }
}
