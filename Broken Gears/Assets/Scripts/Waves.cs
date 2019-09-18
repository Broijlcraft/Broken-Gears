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

    public bool tutorial;

    public void StartCountDown() {

    }

    public void StartNextWave() {

    }
}
