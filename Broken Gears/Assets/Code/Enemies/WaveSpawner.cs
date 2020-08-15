using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    public static WaveSpawner ws_Single;

    public List<Wave> waves = new List<Wave>();

    public bool endlessWave, onlySpawnOne;
    public float spawnDelay, waveDelay;

    [Header("HideInInspector")] public bool waveFunctionality;
    int currentWave, currentEnemy;
    float spawnDelayTimer, waveDelayTimer;

    [HideInInspector] public List<Enemy> enemiesOnTheField = new List<Enemy>();

    private void Awake() {
        ws_Single = this;
    }

    private void Update() {
        if (waveFunctionality) {
            if(currentWave < waves.Count) {
                if(currentEnemy < waves[currentWave].enemies.Count) {
                    if (spawnDelayTimer == 0 || spawnDelayTimer > spawnDelay) {
                        spawnDelayTimer = 0;
                        SpawnNextEnemy();
                    }
                    spawnDelayTimer += Time.deltaTime;
                } else {
                    waveDelayTimer += Time.deltaTime;
                    if(waveDelayTimer > waveDelay) {
                        waveDelayTimer = 0;
                        spawnDelayTimer = 0;
                        if (endlessWave) {
                            ResetWave();
                        } else {
                            NextWave();
                        }
                    }
                }
            }
        }
    }

    public void SpawnNextEnemy() {
        if (waves[currentWave] && waves[currentWave].enemies[currentEnemy]) {
            enemiesOnTheField.Add(Instantiate(waves[currentWave].enemies[currentEnemy], transform.position, Quaternion.identity).GetComponent<Enemy>());
        }
        if (onlySpawnOne) {
            waveFunctionality = false;
        }
        currentEnemy++;
    }

    public void ResetWave() {
        currentEnemy = 0;
        currentEnemy = 0;
    }

    public void NextWave() {
        currentEnemy = 0;
        currentWave++;
    }
}