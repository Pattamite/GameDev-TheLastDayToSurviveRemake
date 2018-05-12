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


    void Start () {
        mainCamera = GetComponent<Camera>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (isFollowPlayer) FollowPlayer();
    }

    private void FollowPlayer () {
        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = halfHeight * mainCamera.aspect;
        float minX = gameHorizontalMin + halfWidth;
        float maxX = gameHorizontalMax - halfWidth;
        float minY = gameVerticalMin + halfHeight;
        float maxY = gameVerticalMax - halfHeight;

        float xPos = Mathf.Clamp(player.transform.position.x, minX, maxX);
        float yPos = Mathf.Clamp(player.transform.position.y, minY, maxY);

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}
