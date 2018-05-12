using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour {

    public bool isOn = true;
    public float zPosition = 0;
    public float range = 50;
    public GameObject playerCharacter;
    public int EnemyLayer = 8;

    private LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        ResetLineRenderer();
        if (isOn) {
            DrawLaser();
        }
    }
    private void DrawLaser () {
        Vector3 currentPosition = this.transform.position;
        float currentRotation = playerCharacter.transform.eulerAngles.z;
        Vector2 direction = new Vector2(Mathf.Cos(currentRotation / 180 * Mathf.PI), Mathf.Sin(currentRotation / 180 * Mathf.PI));
        AddPositionToLineRenderer(currentPosition);

        RaycastHit2D objectHitData = Physics2D.Raycast(currentPosition, direction, range, 1 << EnemyLayer);
        
        if (objectHitData) {
            AddPositionToLineRenderer(objectHitData.point);
        }
        else {
            AddPositionToLineRenderer(currentPosition + new Vector3(direction.x, direction.y) * range);
        }

    }

    private void AddPositionToLineRenderer (Vector3 position) {
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(position.x, position.y, zPosition));
    }

    private void ResetLineRenderer () {
        lineRenderer.positionCount = 0;
    }
}
