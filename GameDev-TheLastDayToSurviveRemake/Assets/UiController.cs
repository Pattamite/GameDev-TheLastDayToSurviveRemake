using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour {
    public Text ammoText;
    public Text reservedAmmoText;
    public Text playerHealthText;
    public Text playerMetalText;

    private Player player;
    private AssaultRifle assaultRifle;

    // Use this for initialization
    void Start () {
        player = GameObject.FindObjectOfType<Player>();
        assaultRifle = GameObject.FindObjectOfType<AssaultRifle>();

    }
	
	// Update is called once per frame
	void Update () {
        ammoText.text = assaultRifle.currentAmmo.ToString();
        reservedAmmoText.text = assaultRifle.pocketAmmo.ToString();
        playerHealthText.text = player.currentHealth.ToString();
        playerMetalText.text = player.currentMetal.ToString();
    }
}
