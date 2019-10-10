using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour {
    
    public float testTime;
    float testTimer;
    public GameObject testEnemy;

    public Transform spawnpoint;
    public int maxWaves;
    public int currentWave;
    public int waveCountDown;
    public int missionIntroTime;
    List<GameObject> activeEnemies = new List<GameObject>();
    float waveCountDownTimer;

    public bool startCountDown;

    public bool endless;
    public bool tutorial;

    private void Start() {
        SpawnEnemy();
    }

    private void Update() {
        testTimer += Time.deltaTime;
        if (testTimer > testTime) {
            SpawnEnemy();
        }
    }

    void EnemieCheck() {
        for (int i = 0; i < activeEnemies.Count; i++) {
            if (activeEnemies[i] == null) {
                activeEnemies.RemoveAt(i);
            }
        }
    }

    void SpawnEnemy() {
        GameObject g = Instantiate(testEnemy, transform.position, Quaternion.identity);
        activeEnemies.Add(g);
        testTimer = 0;
    }

    public void StartNextWave() {

    }
}
