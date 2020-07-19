using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public float spawnDelay;
    public float bspawnDelay;
    public float waveDelay;
    public float bwaveDelay;
    public bool canSpawn;
    public bool changingWave;
    public List<Wave> waves = new List<Wave>();
    public int enemyToGet;
    public int waveToGet;
    public bool staticTutorial;
    public static bool tutorial;

    public static List<GameObject> onTheField = new List<GameObject>();

    private void Start() {
        tutorial = staticTutorial;
    }

    private void Update() {
        if (PlayerLook.canMove == true && canSpawn == true && changingWave == false) {
            if (bspawnDelay < spawnDelay) {
                bspawnDelay += Time.deltaTime;
            } else {
                SpawnEnemy();
                bspawnDelay = 0;
            }
        } else if (changingWave == true) {
            if (bwaveDelay < waveDelay) {
                bwaveDelay += Time.deltaTime;
            } else {
                StartWave();
            }
        }
    }

    public void SpawnEnemy() {
        if (waveToGet < waves.Count && waves.Count > 0 && waves[waveToGet].enemies.Count > 0 && PlayerLook.canMove == true) {
            if (enemyToGet < waves[waveToGet].enemies.Count) {
                GameObject g = Instantiate(waves[waveToGet].enemies[enemyToGet], transform.position, Quaternion.identity);
                onTheField.Add(g);
                enemyToGet++;
            } else {
                NextWave();
            }
        } else if (waveToGet >= waves.Count && onTheField.Count == 0) {
            if (tutorial == true) {
                Manager.uiManager.winTutScreen.SetActive(true);
            } else {
                Manager.uiManager.winGameScreen.SetActive(true);
            }
        }
    }

    void StartWave() {
        changingWave = false;
        canSpawn = true;
        enemyToGet = 0;
        bwaveDelay = 0;
    }

    void NextWave() {
        changingWave = true;
        canSpawn = false;
        waveToGet++;
    }
}
