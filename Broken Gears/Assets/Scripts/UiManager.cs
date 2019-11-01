using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UiManager : MonoBehaviour {
    public List<GameObject> menus = new List<GameObject>();
    public List<Button> towerButtons = new List<Button>();
    public List<GameObject> sliders = new List<GameObject>();
    TowerManager towerManager;
    Button b;
    public static MenuScript staticMenuScript;
    public Text turretText;
    public Text turretValueText;
    public static Text staticTurretText;
    public static Text staticTurretValueText;
    public GameObject fadePic;
    public List<GameObject> disableButtons = new List<GameObject>();
    public static List<GameObject> staticdisableButtonsHolder = new List<GameObject>();

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
            towerManager = GameObject.Find("GameManager").GetComponent<TowerManager>();
            for (i = 0; i < towerButtons.Count; i++) {
                int ib = i;
                towerButtons[ib].onClick.AddListener(() => towerManager.SelectTower(towerManager.towerList[ib]));
            }
        }
        TurnOnOff(false);
    }

    public void TurnOnOff(bool b) {
        for (int i = 0; i < staticdisableButtonsHolder.Count; i++) {
            staticdisableButtonsHolder[i].SetActive(b);
        }
    }

}
