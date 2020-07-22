using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    public List<Wave> waves = new List<Wave>();

    public bool endlessWave;
    public float spawnDelay, waveDelay;

    [Header("HideInInspector")] public bool waveFunctionality;
    int currentWave, currentEnemy;
    float spawnDelayTimer, waveDelayTimer;

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
            Instantiate(waves[currentWave].enemies[currentEnemy], transform.position, Quaternion.identity);
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