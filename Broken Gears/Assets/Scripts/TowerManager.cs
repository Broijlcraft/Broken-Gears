using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

    public List<GameObject> towerss = new List<GameObject>();
    public static List<GameObject> towers = new List<GameObject>();
    public static GameObject selectedTower;
    public GameObject test;
    public static GameObject staticTest;


    private void Awake() {
        towers = towerss;
    }

    public void SelectTower(GameObject tower) {
        selectedTower = tower;
    }
}
