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
    [SerializeField] private int maxEnemyEscapes;
    [HideInInspector] public List<Image> workerImages = new List<Image>();

    public AlarmLight alarmLight;
    private IEnumerator spawner, nextWave;
    private int enemiesEscaped, currentWave;
    [SerializeField] private bool waveFunctionality;
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
    #endregion

    private void Awake() {
        ws_Single = this;

        List<Robot> rList = new List<Robot>();
        for (int i = 0; i < waves.Count; i++) {
            waves[i].Init();
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

        for (int iB = 0; iB < rList.Count; iB++) {
            GameObject prefab = rList[iB].GetPrefab();
            int amount = rList[iB].GetMaxAmount();
            Queue<GameObject> tempQueue = new Queue<GameObject>();
            for (int iC = 0; iC < amount; iC++) {
                GameObject robot = Instantiate(prefab, transform);
                robot.SetActive(false);
                tempQueue.Enqueue(robot);
            }
            robotPool.Add(prefab.name, tempQueue);
        }
    }

    private void Start() {
        alarmLight = FindObjectOfType<AlarmLight>();

        spawner = Spawner();
        nextWave = NextWave();

        //StopCoroutine(spawner);
        //StopCoroutine(nextWave);

        //if (alarmLight) {
        //    alarmLight.SoundAlarm(this);
        //} else {
        //    StartSpawning();
        //}
    }

    private void Update() {
        if (GameManager.gm_Single.devMode && Input.GetButtonDown("Jump")) {
            IncreaseEscaped();
        }
    }

    public void StartSpawning() {
        if (waveFunctionality) {
            StartCoroutine(spawner);
        }
    }

    public IEnumerator Spawner() {
        print("Spawn");
        while (currentWave < waves.Count) {
            print("Curr < ");
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
        print("next");
        StopCoroutine(spawner);
        print("next1");
        if (currentWave < waves.Count) {
            print("Enough");
            yield return new WaitForSeconds(waveDelay);
            print("Enough1");
            currentWave++;
            spawner = Spawner();
            StartCoroutine(spawner);
        } else {
            print("Not Enough");
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
    [Header("Private")] public int amountThisWave;

    public void Init() {
        for (int i = 0; i < robotsThisWave.Count; i++) {
            amountThisWave += robotsThisWave[i].GetMaxAmount();
        }
    }

    public List<Robot> GetRobots() {
        return robotsThisWave;
    }

    public int GetMaxRobotAmount(GameObject prefab) {
        int amount = 0;
        for (int i = 0; i < robotsThisWave.Count; i++) {
            if(robotsThisWave[i].GetPrefab() == prefab) {
                amount = robotsThisWave[i].GetMaxAmount();
                break;
            }
        }
        return amount;
    }

    public int GetMaxAmountThisWave() {
        return amountThisWave;
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
            waveSpawnerScript.workerImages = workerImages;
            Debug.LogWarning("Successfully set workers, don't forget to save!");
        }
    }
}
#endif