using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

    public List<GameObject> towerList = new List<GameObject>();
    public static List<GameObject> towers = new List<GameObject>();
    public GameObject selectedTower;


    private void Awake() {
        towers = towerList;
    }

    public void SelectTower(GameObject tower) {
        selectedTower = tower;
        if (selectedTower.GetComponent<Turret>().turretImg != null) {
            GameObject g = Instantiate(selectedTower.GetComponent<Turret>().turretImg, Vector3.zero, Quaternion.identity);
            g.transform.SetParent(Manager.mobileCanvas);
        }
    }
}
