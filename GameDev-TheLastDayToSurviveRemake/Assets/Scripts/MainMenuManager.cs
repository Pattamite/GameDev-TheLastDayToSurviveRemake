using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    public Text soundText;
    public Text musicText;

    // Use this for initialization
    void Start () {
        if (!PlayerPrefs.HasKey(PlayerPrefKey.IS_AUDIO_ENABLE)) PlayerPrefs.SetInt(PlayerPrefKey.IS_AUDIO_ENABLE, 1);
        if (!PlayerPrefs.HasKey(PlayerPrefKey.IS_MUSIC_ENABLE)) PlayerPrefs.SetInt(PlayerPrefKey.IS_MUSIC_ENABLE, 1);

        UpdateText();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void UpdateText () {
        if (PlayerPrefs.GetInt(PlayerPrefKey.IS_AUDIO_ENABLE) > 0) soundText.text = "Sound : Enable";
        else soundText.text = "Sound : Disable";

        if (PlayerPrefs.GetInt(PlayerPrefKey.IS_MUSIC_ENABLE) > 0) musicText.text = "Music : Enable";
        else musicText.text = "Music : Disable";
    }

    public void ToggleSound () {
        int oldValue = PlayerPrefs.GetInt(PlayerPrefKey.IS_AUDIO_ENABLE);
        PlayerPrefs.SetInt(PlayerPrefKey.IS_AUDIO_ENABLE, (oldValue + 1) % 2);
        UpdateText();
    }

    public void ToggleMusic () {
        int oldValue = PlayerPrefs.GetInt(PlayerPrefKey.IS_MUSIC_ENABLE);
        PlayerPrefs.SetInt(PlayerPrefKey.IS_MUSIC_ENABLE, (oldValue + 1) % 2);
        UpdateText();
    }
}
