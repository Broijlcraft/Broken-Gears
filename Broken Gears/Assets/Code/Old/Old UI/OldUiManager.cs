using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OldUiManager : MonoBehaviour {
    public List<GameObject> menus = new List<GameObject>(), sliders = new List<GameObject>();
    public List<Button> towerButtons = new List<Button>();
    OldTowerManager towerManager;
    public static MenuScript staticMenuScript;
    public Text turretText;
    public Text turretValueText;
    public static Text staticTurretText;
    public static Text staticTurretValueText;
    public GameObject fadePic;
    public List<GameObject> disableButtons = new List<GameObject>();
    public static List<GameObject> staticdisableButtonsHolder = new List<GameObject>();

    [Header("Escaped")]

    public int maxEscaped;
    public static int currentEscaped;
    public List<Image> workerPic = new List<Image>();
    public GameObject gameOverScreen, winGameScreen, winTutScreen;
    public static bool gameOver;

    int i;
    
    private void Awake() {
        fadePic.SetActive(true);
        staticMenuScript = GameObject.Find("Canvas").GetComponentInChildren<MenuScript>();
        staticTurretText = turretText;
        staticTurretValueText = turretValueText;
        staticdisableButtonsHolder = disableButtons;
    }

    private void Start() {
        if (SceneManager.GetActiveScene().name != "MainMenu") {
            towerManager = GameObject.Find("GameManager").GetComponent<OldTowerManager>();
            for (i = 0; i < towerButtons.Count; i++) {
                int ib = i;
                towerButtons[ib].onClick.AddListener(() => towerManager.SelectTower(towerManager.towerList[ib]));
            }
        }
        TurnOnOff(false);
    }

    public void IncreaseEscaped(int i) {
        currentEscaped += i;
        if (currentEscaped < 6) {
            workerPic[workerPic.Count - currentEscaped].color = Color.red;
            GameObject.Find("FactoryLight").GetComponent<AudioSource>().Play();
            InvokeRepeating("StopMusic", GameObject.Find("FactoryLight").GetComponent<AudioSource>().clip.length, 0);
            if (currentEscaped == 5) {
                GameOver();
            }
        }
    }

    void StopMusic() {
        GameObject.Find("FactoryLight").GetComponent<AudioSource>().Stop();
    }

    public void GameOver() {
        Time.timeScale = 0;
        gameOver = true;
        gameOverScreen.SetActive(true);
    }

    public void TurnOnOff(bool b) {
        for (int i = 0; i < staticdisableButtonsHolder.Count; i++) {
            staticdisableButtonsHolder[i].SetActive(b);
        }
    }
}
