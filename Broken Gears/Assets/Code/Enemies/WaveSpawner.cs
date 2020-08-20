using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
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


    [Header("Workers")]
    public Transform uiWorkersHolder;
    public int maxEnemyEscapes;
    int enemiesEscaped;
    [HideInInspector] public List<Image> workerImages = new List<Image>();
    public AlarmLight factoryWarningLight;

    private void Awake() {
        ws_Single = this;
    }

    private void Start() {
        factoryWarningLight = FindObjectOfType<AlarmLight>();
    }

    private void Update() {
        if (GameManager.gm_Single.devMode && Input.GetButtonDown("Jump")) {
            IncreaseEscaped();
        }

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

    public void StartSpawnSequence() {
        if (factoryWarningLight) {
            factoryWarningLight.soundAlarm = true;
        } else {
            waveFunctionality = true;
        }
    }

    public void SpawnNextEnemy() {
        if (waves[currentWave] && waves[currentWave].enemies[currentEnemy]) {
            GameObject go = Instantiate(waves[currentWave].enemies[currentEnemy], transform.position, Quaternion.identity);
            Enemy enemy = go.GetComponent<Enemy>();
            enemiesOnTheField.Add(enemy);
        }
        if (onlySpawnOne) {
            waveFunctionality = false;
        }
        currentEnemy++;
    }

    public void IncreaseEscaped() {
        if(enemiesEscaped < maxEnemyEscapes) {
            workerImages[workerImages.Count - (1 + enemiesEscaped)].color = Color.red;
        }
        enemiesEscaped++;
        if(enemiesEscaped == maxEnemyEscapes) {
            GameManager.gm_Single.SetGameOver(GameManager.GameOverState.Failure);
        }
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

#if UNITY_EDITOR
[CustomEditor(typeof(WaveSpawner))]
public class WorkerEditor : Editor {
    WaveSpawner waveSpawnerScript;

    private void OnEnable() {
        waveSpawnerScript = (WaveSpawner)target;
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if (GUILayout.Button("Set workers")) {
            List<Image> workerImages = new List<Image>();
            if (waveSpawnerScript.uiWorkersHolder) {
                for (int i = 0; i < waveSpawnerScript.uiWorkersHolder.childCount; i++) {
                    Image workerImage = waveSpawnerScript.uiWorkersHolder.GetChild(i).GetComponent<Image>();
                    workerImages.Add(workerImage);
                    EditorUtility.SetDirty(workerImage);
                }
            }
            waveSpawnerScript.workerImages = workerImages;
            Debug.LogWarning("Successfully set workers, don't forget to save!");
        }
    }
}
#endif