using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour {
    public int maxHP = 20;
    public AudioClip destroySound;

    private int currentHP;

	void Start () {
        currentHP = maxHP;
    }
	
	void Update () {
		
	}

    public void GetHit (int damage) {
        currentHP -= damage;
        CheckDead();
    }

    private void CheckDead () {
        if(currentHP <= 0) {
            if(PlayerPrefs.GetInt(PlayerPrefKey.IS_AUDIO_ENABLE) > 0) {
                AudioSource.PlayClipAtPoint(destroySound, transform.position, 2.0f);
            }
            Destroy(gameObject);
        }
    }
}
