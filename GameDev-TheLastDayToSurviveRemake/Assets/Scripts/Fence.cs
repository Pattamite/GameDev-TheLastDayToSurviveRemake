using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour {
    public int maxHP = 20;
    public AudioClip destroySound;
    public AudioClip deploySound;
    public AudioClip hitSound;

    private int currentHP;

	void Start () {
        currentHP = maxHP;
        if (PlayerPrefs.GetInt(PlayerPrefKey.IS_AUDIO_ENABLE) > 0) {
            AudioSource.PlayClipAtPoint(deploySound, transform.position);
        }
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
                AudioSource.PlayClipAtPoint(destroySound, transform.position, 0.7f);
            }
            Destroy(gameObject);
        }
        else {
            if (PlayerPrefs.GetInt(PlayerPrefKey.IS_AUDIO_ENABLE) > 0) {
                AudioSource.PlayClipAtPoint(hitSound, transform.position, 0.5f);
            }
        }
    }
}
