using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    public static WaveSpawner ws_Single;

    [SerializeField] private List<RobotWave> waves = new List<RobotWave>();

    [Space, SerializeField] private float globalEnemySpeedMultiplier = 1f;

    [SerializeField] private float spawnDelay, waveDelay;

    [Space, SerializeField] private GameObject mobileUiHealthPrefab;

    [Header("Workers"), SerializeField] private Transform uiWorkersHolder;
    [SerializeField] private List<Image> workerImages = new List<Image>();

    private int maxEnemyEscapes;
    private AlarmLight alarmLight;
    private bool waveFunctionality;
    private IEnumerator spawner, nextWave;
    private int enemiesEscaped, currentWave;
    [SerializeField] private List<Robot> rList = new List<Robot>();
    private Dictionary<string, Queue<GameObject>> robotPool = new Dictionary<string, Queue<GameObject>>();

    [HideInInspector] public List<Enemy> enemiesOnTheField = new List<Enemy>();

    #region Get/Set
    public float GetGlobalEnemySpeedMultiplier() {
        return globalEnemySpeedMultiplier;
    }

    public Transform GetUiWorkersHolder() {
        return uiWorkersHolder;
    }

    public GameObject GetMobileUiHealtPrefab() {
        return mobileUiHealthPrefab;
    }

    public void SetWaveFunctionality(bool state) {
        waveFunctionality = state;
    }

    public void SetWorkerImages(List<Image> images) {
        workerImages = images;
    }
    #endregion

    private void Awake() {
        ws_Single = this;

        for (int i = 0; i < waves.Count; i++) {
            if (rList.Count > 0) {
                List<Robot> tempList = waves[i].GetRobots();
                for (int iB = 0; iB < tempList.Count; iB++) {
                    for (int iC = 0; iC < rList.Count; iC++) {
                        GameObject pf = tempList[iB].GetPrefab();
                        int index = ContainsWhere(rList, pf);
                        if (index != -1) {
                            int tempMax = tempList[iB].GetMaxAmount();
                            Robot botInWave = rList[iC];
                            int newAmount = botInWave.GetMaxAmount() + tempMax;
                            botInWave.SetMaxAmount(newAmount);                            
                        } else {
                            rList.Add(tempList[iB]);
                        }
                    }
                }
            } else {
                List<Robot> tempBots = waves[i].GetRobots();
                for (int iB = 0; iB < tempBots.Count; iB++) {
                    Robot bot = tempBots[iB].CopyAsNew();
                    rList.Add(bot);
                }
            }
        }
    }

    private void Start() {
        for (int iB = 0; iB < rList.Count; iB++) {
            GameObject prefab = rList[iB].GetPrefab();
            int amount = rList[iB].GetMaxAmount();
            Queue<GameObject> tempQueue = new Queue<GameObject>();
            for (int iC = 0; iC < amount; iC++) {
                GameObject robot = Instantiate(prefab, transform);
                //robots disable on their own in Start()
                tempQueue.Enqueue(robot);
            }
            robotPool.Add(prefab.name, tempQueue);
        }

        alarmLight = FindObjectOfType<AlarmLight>();

        spawner = Spawner();
        nextWave = NextWave();
        maxEnemyEscapes = workerImages.Count;
    }

    public void StartSpawning() {
        if (waveFunctionality) {
            StartCoroutine(spawner);
        }
    }

    public IEnumerator Spawner() {
        while (currentWave < waves.Count) {
            RobotWave wave = waves[currentWave];
            int count = wave.GetRobots().Count;
            if (count > 0) {
                SpawnNextEnemy(wave);
            } else {
                StartCoroutine(nextWave);
                break;
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator NextWave() {
        StopCoroutine(spawner);
        if (currentWave < waves.Count) {
            yield return new WaitForSeconds(waveDelay);
            currentWave++;
            spawner = Spawner();
            StartCoroutine(spawner);
        }
    }

    int ContainsWhere(List<Robot> rList, GameObject robot) {
        int index = -1;
        for (int i = 0; i < rList.Count; i++) {
            if(rList[i].GetPrefab() == robot) {
                return i;
            }
        }
        return index;
    }

    public void StartSpawnSequence() {
        waveFunctionality = true;
        if (alarmLight) {
            alarmLight.SoundAlarm(this);
        } else {
            StartSpawning();
        }
    }

    public void SpawnNextEnemy(RobotWave wave) {
        List<Robot> robots = wave.GetRobots();
        int rand = Random.Range(0, robots.Count);
        Robot robot = robots[rand];
        string tag = robot.GetPrefab().name;
        if (robotPool.ContainsKey(tag)) {
            GameObject robotObj = robotPool[tag].Dequeue();
            robotPool[tag].Enqueue(robotObj);
            Enemy enemy = robotObj.GetComponent<Enemy>();
            enemiesOnTheField.Add(enemy);
            enemy.Init();
            robotObj.SetActive(true);
            wave.SetAmountUsedInRobot(rand, robot.GetUsed() + 1);
        }
    }

    public void RemoveEnemy(Enemy enemy, bool wasKilled) {
        if (enemiesOnTheField.Contains(enemy)) {
            enemiesOnTheField.Remove(enemy);
            if (wasKilled) {

            }
        }
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
}

[System.Serializable]
public class RobotWave {
    [SerializeField] private List<Robot> robotsThisWave = new List<Robot>();

    public List<Robot> GetRobots() {
        return robotsThisWave;
    }

    public void SetAmountUsedInRobot(int index, int amount) {
        if(robotsThisWave[index].GetMaxAmount() > amount) {
            robotsThisWave[index].SetUsed(amount);
        } else {
            robotsThisWave.RemoveAt(index);
        }
    }
}

[System.Serializable]
public class Robot {
    [SerializeField] private GameObject prefab;
    [SerializeField] private int maxAmount, used;

    public Robot CopyAsNew() {
        Robot bot = new Robot {
            prefab = prefab,
            maxAmount = maxAmount,
            used = used
        };
        return bot;
    }

    public GameObject GetPrefab() {
        return prefab;
    }

    public int GetMaxAmount() {
        return maxAmount;
    }

    public void SetMaxAmount(int amount) {
        maxAmount = amount;
    }

    public int GetUsed() {
        return used;
    }

    public void SetUsed(int amount) {
        used = amount;
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
            Transform holder = waveSpawnerScript.GetUiWorkersHolder();
            if (holder) {
                for (int i = 0; i < holder.childCount; i++) {
                    Image workerImage = holder.GetChild(i).GetComponent<Image>();
                    workerImages.Add(workerImage);
                    EditorUtility.SetDirty(workerImage);
                }
            }
            waveSpawnerScript.SetWorkerImages(workerImages);
            Debug.LogWarning("Successfully set workers, don't forget to save!");
        }
    }
}
#endif