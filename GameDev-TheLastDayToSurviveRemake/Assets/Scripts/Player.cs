using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Camera mainCamera;

    void Start () {
        mainCamera = Camera.main;

    }

    void Update () {
        UpdateRotation();

    }

    private void UpdateRotation () {
        Vector3 mousePosWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 currentPosition = transform.position;
        Vector3 currentRotation = transform.eulerAngles;

        float deltaX = mousePosWorld.x - currentPosition.x;
        float deltaY = mousePosWorld.y - currentPosition.y;

        float rotationAngle = Mathf.Atan2(deltaY, deltaX) / Mathf.PI * 180f;
        transform.eulerAngles = new Vector3(currentRotation.x, currentRotation.y, rotationAngle);
    }
}
