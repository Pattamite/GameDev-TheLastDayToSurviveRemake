using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour {
    public Text ammoText;
    public Text reservedAmmoText;
    public Text playerHealthText;
    public Text playerMetalText;
    public Text phaseText;
    public Text waveCounterText;
    public Text counterText;
    public Text countText;

    private Player player;
    private AssaultRifle assaultRifle;
    private GameStateController gameStateController;

    // Use this for initialization
    void Start () {
        player = GameObject.FindObjectOfType<Player>();
        assaultRifle = GameObject.FindObjectOfType<AssaultRifle>();
        gameStateController = GameObject.FindObjectOfType<GameStateController>();
    }
	
	// Update is called once per frame
	void Update () {
        ammoText.text = assaultRifle.currentAmmo.ToString();
        reservedAmmoText.text = assaultRifle.pocketAmmo.ToString();
        playerHealthText.text = player.currentHealth.ToString();
        playerMetalText.text = player.currentMetal.ToString();
        if(gameStateController.currentState == GameStateController.STATE_COMBAT) {
            phaseText.text = "Combat Phase";
            counterText.text = "Enemy :";
            countText.text = GameStateController.currentZombieLeft.ToString();
        }
        else {
            phaseText.text = "Prep Phase";
            counterText.text = "Time :";
            countText.text = gameStateController.GetTimeLeft().ToString();
        }
        waveCounterText.text = gameStateController.currentWave.ToString() + "/" + gameStateController.totalWave.ToString();
    }
}
