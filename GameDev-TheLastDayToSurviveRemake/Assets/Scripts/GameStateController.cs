using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour {
    public static int STATE_PREP = 0;
    public static int STATE_COMBAT = 1;
    public AudioClip prepSong;
    public AudioClip combatSong1;
    public AudioClip combatSong2;
    public int totalWave = 5;
    public float prepTime = 59.9f;
    public int baseZombieCount = 10;
    public int incrementZombieCount = 10;
    public static int currentZombieLeft;
    public float spawnDelay = 0.5f;
    public float spawnXRange;
    public float spawnYRange;
    public float spawnX1;
    public float spawnX2;
    public float spawnY1;
    public float spawnY2;
    public Object zombie;

    private AudioSource audioSource;
    private int currentCombatSong;
    public int currentState { get; private set;}
    public int currentWave { get; private set; }
    private float prepTimeLeft;
    private Player player;
    private int zombieNotSpawn;
    private float currentSpawnDelay;
	
	void Start () {
        if (!PlayerPrefs.HasKey(PlayerPrefKey.IS_MUSIC_ENABLE)) PlayerPrefs.SetInt(PlayerPrefKey.IS_MUSIC_ENABLE, 1);
        audioSource = GetComponent<AudioSource>();
        currentCombatSong = 1;
        currentWave = 1;
        player = GameObject.FindObjectOfType<Player>();
        SetUpPrepPhase();
    }
	
	void Update () {
		if(currentState == STATE_PREP) {
            prepTimeLeft -= Time.deltaTime;
            if(prepTimeLeft <= 0f) {
                SetUpCombatPhase();
            }
        }
        if(currentState == STATE_COMBAT) {
            if(currentZombieLeft <= 0) {
                if (currentWave == totalWave) {
                    //TODO
                }
                else {
                    currentWave++;
                    SetUpPrepPhase();
                }
            }
            else {
                if(zombieNotSpawn > 0 && currentSpawnDelay <= 0) {
                    currentSpawnDelay = spawnDelay;
                    SpawnZombie();
                }
                else {
                    currentSpawnDelay -= Time.deltaTime;
                }
            }
        }
	}

    private void SetUpPrepPhase () {
        if (PlayerPrefs.GetInt(PlayerPrefKey.IS_MUSIC_ENABLE) > 0) {
            audioSource.Stop();
            audioSource.clip = prepSong;
            audioSource.Play();
        }
            
        currentState = STATE_PREP;
        prepTimeLeft = prepTime;
        player.SetPlayerState(Player.STATE_PREP);
    }

    private void SetUpCombatPhase () {
        if (PlayerPrefs.GetInt(PlayerPrefKey.IS_MUSIC_ENABLE) > 0) {
            audioSource.Stop();
            if (currentCombatSong == 0) audioSource.clip = combatSong1;
            else audioSource.clip = combatSong2;
            currentCombatSong = (currentCombatSong + 1) % 2;
            audioSource.Play();
        }
        currentState = STATE_COMBAT;
        currentSpawnDelay = 0;
        currentZombieLeft = baseZombieCount + (incrementZombieCount * currentWave);
        zombieNotSpawn = currentZombieLeft;
        player.SetPlayerState(Player.STATE_COMBAT);
    }

    private void SpawnZombie () {
        zombieNotSpawn--;
        int randomValue = Random.Range(0, 4);
        float randomAxis;
        if(randomValue == 0) {
            randomAxis = Random.Range(-spawnXRange, spawnXRange);
            Instantiate(zombie, new Vector3(randomAxis, spawnY1, 0), Quaternion.identity);
        }
        else if (randomValue == 1) {
            randomAxis = Random.Range(-spawnXRange, spawnXRange);
            Instantiate(zombie, new Vector3(randomAxis, spawnY2, 0), Quaternion.identity);
        }
        else if (randomValue == 2) {
            randomAxis = Random.Range(-spawnYRange, spawnYRange);
            Instantiate(zombie, new Vector3(spawnX1, randomAxis, 0), Quaternion.identity);
        }
        else {
            randomAxis = Random.Range(-spawnYRange, spawnYRange);
            Instantiate(zombie, new Vector3(spawnX2, randomAxis, 0), Quaternion.identity);
        }
        
    }

    public int GetTimeLeft () {
        return (int)prepTimeLeft + 1;
    }
}
