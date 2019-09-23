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
    List<GameObject> enemiesLeft = new List<GameObject>();
    float waveCountDownTimer;

    public bool startCountDown;

    public bool tutorial;

    private void Update() {
        testTimer += Time.deltaTime;
        if (testTimer > testTime) {
            GameObject g = Instantiate(testEnemy, transform.position, Quaternion.identity);
            enemiesLeft.Add(g);
            testTimer = 0;
        }
    }

    public void StartNextWave() {

    }
}
