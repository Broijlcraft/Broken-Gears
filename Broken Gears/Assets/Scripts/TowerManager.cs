using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

    public List<GameObject> towerList = new List<GameObject>();
    public static List<GameObject> towers = new List<GameObject>();
    public static GameObject selectedTower;


    private void Awake() {
        towers = towerList;
    }

    public void SelectTower(GameObject tower) {
        if (tower.GetComponent<SelectTowerPlacement>().scrapValue < ScrapEconomy.currentScrapValue || Manager.devMode == false) {
            if (selectedTower != null) {
                Destroy(selectedTower);
            }
            selectedTower = Instantiate(tower);
            if (selectedTower.GetComponent<Turret>().turretImg != null) {
                GameObject turretImg = Instantiate(selectedTower.GetComponent<Turret>().turretImg, Vector3.zero, Quaternion.identity);
                turretImg.transform.SetParent(Manager.mobileCanvas);
            }
        }
    }
}
