using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public FencePreview fencePreview;
    public float fencePreviewDistance = 1f;
    public Object fence;
    public Slider reloadSlider;
    public Vector3 reloadSliderOffset;
    public int maxHealth = 100;

    public int currentHealth { get; private set; }

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

        currentHealth = maxHealth;

        currentState = STATE_PREP;
        SetupState();
    }

    void Update () {
        UpdateMovement();
        UpdateRotation();
        UpdateReloadSlider();
        if (Input.GetKeyDown(KeyCode.Tab)) {
            currentState = (currentState + 1) % 2;
            SetupState();
        }
        if (currentState == STATE_PREP) {
            SetFencePreview();
        }
        if (currentState == STATE_COMBAT) {
            UpdateAttack();
        }
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

    private void UpdateReloadSlider () {
        if (assaultRifle.isReloading) {
            reloadSlider.gameObject.GetComponent<RectTransform>().anchoredPosition = transform.position + reloadSliderOffset;
            reloadSlider.value = assaultRifle.ReloadProgress();
        }
        else {
            reloadSlider.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(100, 100, 100);
        }
        
    }

    private void SetFencePreview () {
        float rotation = playerCharacter.transform.eulerAngles.z;
        float xPos = playerCharacter.transform.position.x + Mathf.Cos(rotation / 180f * Mathf.PI);
        float yPos = playerCharacter.transform.position.y + Mathf.Sin(rotation / 180f * Mathf.PI);

        fencePreview.gameObject.transform.eulerAngles = new Vector3(0, 0, rotation);
        fencePreview.gameObject.transform.position = new Vector3(xPos, yPos, 0);

        DeployFence(xPos, yPos, rotation);
    }

    private void DeployFence (float xPos, float yPos, float rotation) {
        if(Input.GetButtonDown("Fire1") && fencePreview.isDeployable) {
            Instantiate(fence, new Vector3(xPos, yPos, 0), Quaternion.Euler(0, 0, rotation));
        }
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
            fencePreview.gameObject.SetActive(true);
        }
        else if (currentState == STATE_COMBAT) {
            laserSight.isOn = true;
            fencePreview.gameObject.SetActive(false);
        }
    }

    public void GetHit(int damage) {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        CheckDead();
    }

    private void CheckDead () {
        if(currentHealth <= 0) {
            //TODO
        }
    }
}
