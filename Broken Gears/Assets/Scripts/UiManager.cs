using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UiManager : MonoBehaviour {
    public GameObject menusHolder;
    public GameObject menu;
    public GameObject optionsMenu;
    public List<Button> towerButtons = new List<Button>();
    TowerManager towerManager;

    int i;

    private void Start() {
        towerManager = GameObject.Find("Manager").GetComponent<TowerManager>();
        for (i = 0; i < towerButtons.Count; i++) {
            towerButtons[i].onClick.AddListener(()=>towerManager.SelectTower(towerManager.towerList[i]));
        }
    }
}
