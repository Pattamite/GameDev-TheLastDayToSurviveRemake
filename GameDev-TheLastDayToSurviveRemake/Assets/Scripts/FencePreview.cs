using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencePreview : MonoBehaviour {
    public SpriteRenderer spriteRenderer;
    public Sprite okSprite;
    public Sprite notOkSprite;
    public bool isDeployable {get; private set;}

	// Use this for initialization
	void Start () {
        isDeployable = true;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D (Collider2D collision) {
        if (collision.gameObject.GetComponent<Fence>()) {
            isDeployable = false;
            spriteRenderer.sprite = notOkSprite;
        }
    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.gameObject.GetComponent<Fence>()) {
            isDeployable = true;
            spriteRenderer.sprite = okSprite;
        }
    }
}
