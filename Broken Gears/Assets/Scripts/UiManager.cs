using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UiManager : MonoBehaviour {
    public List<GameObject> menus = new List<GameObject>();
    public List<Button> towerButtons = new List<Button>();
    public List<GameObject> sliders = new List<GameObject>();
    TowerManager towerManager;
    public static MenuScript staticMenuScript;

    int i;

    private void Awake() {
        staticMenuScript = GameObject.Find("Canvas").GetComponentInChildren<MenuScript>();
    }

    private void Start() {
        towerManager = GameObject.Find("GameManager").GetComponent<TowerManager>();
        for (i = 0; i < towerButtons.Count; i++) {
            int ib = i;
            towerButtons[ib].onClick.AddListener(() => towerManager.SelectTower(towerManager.towerList[ib]));
        }
    }
}
