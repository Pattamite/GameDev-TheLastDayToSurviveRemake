using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject player;
    public float gameHorizontalMin;
    public float gameHorizontalMax;
    public float gameVerticalMin;
    public float gameVerticalMax;
    public bool isFollowPlayer;

    private Camera mainCamera;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;


    void Start () {
        mainCamera = GetComponent<Camera>();
        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = halfHeight * mainCamera.aspect;
        minX = gameHorizontalMin + halfWidth;
        maxX = gameHorizontalMax - halfWidth;
        minY = gameVerticalMin + halfHeight;
        maxY = gameVerticalMax - halfHeight;
    }
	
	// Update is called once per frame
	void Update () {
        if (isFollowPlayer) FollowPlayer();
    }

    private void FollowPlayer () {
        float xPos = Mathf.Clamp(player.transform.position.x, minX, maxX);
        float yPos = Mathf.Clamp(player.transform.position.y, minY, maxY);

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}
