﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UiManager : MonoBehaviour {
    public GameObject menusHolder;
    public GameObject menu;
    public GameObject optionsMenu;
    public GameObject videoMenu;
    public GameObject audioMenu;
    public List<Button> towerButtons = new List<Button>();
    public List<GameObject> sliders = new List<GameObject>();
    TowerManager towerManager;
    public Button b;
    public GameObject g;

    int i;

    private void Start() {
        towerManager = GameObject.Find("GameManager").GetComponent<TowerManager>();
        for (i = 0; i < towerButtons.Count; i++) {
            int ib = i;
            towerButtons[ib].onClick.AddListener(() => towerManager.SelectTower(towerManager.towerList[ib]));
        }
    }
}
