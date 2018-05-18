using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject playerCharacter;
    public AssaultRifle assaultRifle;
    public float movementSpeed = 1f;
    public float movementBoxSize = 0.3f;
    public float gameHorizontalMin;
    public float gameHorizontalMax;
    public float gameVerticalMin;
    public float gameVerticalMax;
    public LaserSight laserSight;

    private static int STATE_PREP = 0;
    private static int STATE_COMBAT = 1;

    private Camera mainCamera;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private int currentState;

    void Start () {
        mainCamera = Camera.main;

        minX = gameHorizontalMin + movementBoxSize;
        maxX = gameHorizontalMax - movementBoxSize;
        minY = gameVerticalMin + movementBoxSize;
        maxY = gameVerticalMax - movementBoxSize;
        currentState = STATE_PREP;
        SetupState();
    }

    void Update () {
        UpdateMovement();
        UpdateRotation();
        if (Input.GetKeyDown(KeyCode.Tab)) {
            currentState = (currentState + 1) % 2;
            SetupState();
        }
        if (currentState == STATE_COMBAT) UpdateAttack();
    }

    private void UpdateRotation () {
        Vector3 mousePosWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 currentPosition = transform.position;
        Vector3 currentRotation = playerCharacter.transform.eulerAngles;

        float deltaX = mousePosWorld.x - currentPosition.x;
        float deltaY = mousePosWorld.y - currentPosition.y;

        float rotationAngle = Mathf.Atan2(deltaY, deltaX) / Mathf.PI * 180f;
        playerCharacter.transform.eulerAngles = new Vector3(currentRotation.x, currentRotation.y, rotationAngle);
    }

    private void UpdateMovement () {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, moveY, 0);
        if (movement.magnitude > 1) movement = movement.normalized;

        transform.Translate(movement * movementSpeed * Time.deltaTime);

        float xPos = Mathf.Clamp(transform.position.x, minX, maxX);
        float yPos = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }

    private void UpdateAttack () {
        if(Input.GetAxisRaw("Fire1") == 1) {
            assaultRifle.PullTrigger();
        }
        else {
            assaultRifle.ReleaseTrigger();
        }

        if (Input.GetAxisRaw("Reload") == 1) {
            assaultRifle.Reload();
        }
    }

    private void SetupState () {
        if(currentState == STATE_PREP) {
            laserSight.isOn = false;
        }
        else if (currentState == STATE_COMBAT) {
            laserSight.isOn = true;
        }
    }
}
