using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldWaveSpawn : MonoBehaviour {

    public static OldWaveSpawn old_wv_Single;
    public bool endless;


    public float spawnDelay, bspawnDelay, waveDelay, bwaveDelay;
    public bool canSpawn, changingWave;
    public List<Wave> waves = new List<Wave>();
    public int enemyToGet, waveToGet;
    public bool isTutorial;

    public List<GameObject> onTheField = new List<GameObject>();

    private void Awake() {
        old_wv_Single = this;
    }

    private void Update() {
        if (canSpawn == true && changingWave == false) {
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

    public delegate void TestDel(float maxV);

    TestDel Spawn;
    TestDel Start;

    public void SpawnEnemy() {
        if (waveToGet < waves.Count && waves.Count > 0 && waves[waveToGet].enemies.Count > 0) {
            if (enemyToGet < waves[waveToGet].enemies.Count) {
                GameObject g = Instantiate(waves[waveToGet].enemies[enemyToGet], transform.position, Quaternion.identity);
                onTheField.Add(g);
                enemyToGet++;
            } else {
                if (endless) {
                    enemyToGet = 0;
                } else {
                    NextWave();
                }
            }
        } else if (waveToGet >= waves.Count && onTheField.Count == 0) {
            if (isTutorial == true) {
                //OldUiManager.old_um_Single.winTutScreen.SetActive(true);
            } else {
                //OldUiManager.old_um_Single.winGameScreen.SetActive(true);
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
