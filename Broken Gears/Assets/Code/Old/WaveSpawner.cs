using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public static WaveSpawner wv_Single;

    public float spawnDelay, bspawnDelay, waveDelay, bwaveDelay;
    public bool canSpawn, changingWave;
    public List<Wave> waves = new List<Wave>();
    public int enemyToGet, waveToGet;
    public bool isTutorial;

    public static List<GameObject> onTheField = new List<GameObject>();

    private void Awake() {
        wv_Single = this;
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
            if (isTutorial == true) {
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
