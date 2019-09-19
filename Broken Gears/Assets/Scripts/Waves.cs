using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour {

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
        if (Input.GetButtonDown("Jump")) {
            startCountDown = true;
        }
        if (startCountDown == true) {
            if (waveCountDownTimer < waveCountDown) {
                waveCountDownTimer += Time.deltaTime;
            } else {
                startCountDown = false;
                print("DaKing");
                waveCountDownTimer = 0;
            }
        }
    }

    public void StartNextWave() {

    }
}
