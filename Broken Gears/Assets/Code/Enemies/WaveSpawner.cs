using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    public static WaveSpawner singleWS;

    [SerializeField] private List<RobotWave> waves = new List<RobotWave>();
    [SerializeField] private List<RobotWave> wavesBackUp = new List<RobotWave>();

    [Space, SerializeField] private float globalEnemySpeedMultiplier = 1f;

    [SerializeField] private float spawnDelay, waveDelay;

    [Space, SerializeField] private GameObject mobileUiHealthPrefab;

    [Header("Workers"), SerializeField] private Transform uiWorkersHolder;
    [SerializeField] private List<Image> workerImages = new List<Image>();

    private int maxEnemyEscapes;
    private AlarmLight alarmLight;
    private IEnumerator spawner;
    private int enemiesEscaped, currentWave;
    private List<Robot> rList = new List<Robot>();
    [HideInInspector] public List<Enemy> enemiesOnTheField = new List<Enemy>();
    private List<Enemy> allEnemies = new List<Enemy>();
    private Dictionary<string, Queue<GameObject>> robotPool = new Dictionary<string, Queue<GameObject>>();

    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject restartButtonHolder;


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

    public void SetWorkerImages(List<Image> images) {
        workerImages = images;
    }
    #endregion

    private void Awake() {
        singleWS = this;

        for (int i = 0; i < waves.Count; i++) {
            wavesBackUp.Add(waves[i].CopyAsNew());
            if (rList.Count > 0) {
                List<Robot> tempList = waves[i].GetRobots();
                for (int iB = 0; iB < tempList.Count; iB++) {
                    GameObject pf = tempList[iB].GetPrefab();
                    int index = ContainsWhere(rList, pf);
                    if (index > -1) {
                        int tempMax = tempList[iB].GetMaxAmount();
                        Robot botInWave = rList[index];
                        int newAmount = botInWave.GetMaxAmount() + tempMax;
                        botInWave.SetMaxAmount(newAmount);
                    } else {
                        rList.Add(tempList[iB].CopyAsNew());
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
        if (restartButton) {
            restartButton.onClick.AddListener(Restart);
        }
        restartButtonHolder.SetActive(false);
        for (int iB = 0; iB < rList.Count; iB++) {
            GameObject prefab = rList[iB].GetPrefab();
            int amount = rList[iB].GetMaxAmount();
            Queue<GameObject> tempQueue = new Queue<GameObject>();
            for (int iC = 0; iC < amount; iC++) {
                GameObject robot = Instantiate(prefab, transform);
                //robots disable in their Start()
                Enemy enemy = robot.GetComponent<Enemy>();
                allEnemies.Add(enemy);
                tempQueue.Enqueue(robot);
            }
            robotPool.Add(prefab.name, tempQueue);
        }

        alarmLight = FindObjectOfType<AlarmLight>();

        maxEnemyEscapes = workerImages.Count;
    }

    public void Restart() {
        print("restart");
        restartButtonHolder.SetActive(false);
        StopAllCoroutines();
        waves.Clear();
        for (int i = 0; i < wavesBackUp.Count; i++) {
            waves.Add(wavesBackUp[i].CopyAsNew());
        }
        for (int i = 0; i < allEnemies.Count; i++) {
            Enemy enemy = allEnemies[i];
            enemy.Death(true);
        }
        currentWave = 0;
        //TowerManager.singleTM.Restart();
        ScrapManager.sm_single.Restart();
        StartSpawnSequence();
    }

    public void StartSpawning() {
        spawner = Spawner();
        StartCoroutine(spawner);
    }

    public IEnumerator Spawner() {
        //restartButtonHolder.SetActive(true);
        while (currentWave < waves.Count) {
            RobotWave wave = waves[currentWave];
            int count = wave.GetRobots().Count;
            if (count > 0) {
                SpawnNextEnemy(wave);
            } else {
                StartCoroutine(NextWave());
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

    int ContainsWhere(List<Robot> robotList, GameObject robot) {
        int index = -1;
        RobotType type = robot.GetComponent<Enemy>().GetRobotType();
        for (int i = 0; i < robotList.Count; i++) {
            RobotType rType = robotList[i].GetPrefab().GetComponent<Enemy>().GetRobotType();
            if (rType == type) { 
                index = i;
                break;
            }
        }
        return index;
    }

    public void StartSpawnSequence() {
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

    public void RemoveEnemy(Enemy enemy) {
        if (enemiesOnTheField.Contains(enemy)) {
            enemiesOnTheField.Remove(enemy);
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

    public RobotWave CopyAsNew() {
        RobotWave wave = new RobotWave();
        wave.SetRobotsThisWave(robotsThisWave);
        return wave;
    }

    public List<Robot> GetRobots() {
        return robotsThisWave;
    }

    public void SetRobotsThisWave(List<Robot>robots) {
        for (int i = 0; i < robots.Count; i++) {
            robotsThisWave.Add(robots[i].CopyAsNew());
        }
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

    #region Get/Set
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
    #endregion

    public Robot CopyAsNew() {
        Robot bot = new Robot {
            prefab = prefab,
            maxAmount = maxAmount,
            used = used
        };
        return bot;
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