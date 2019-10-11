using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

    public List<GameObject> towerList = new List<GameObject>();
    public static List<GameObject> towers = new List<GameObject>();
    public static GameObject selectedTower;
    public Vector4 canPlaceColor;
    public Vector4 canNotPlaceColor;
    public static Vector4 canPlace; 
    public static Vector4 canNotPlace; 

    private void Awake() {
        towers = towerList;
        canPlace = canPlaceColor;
        canNotPlace = canNotPlaceColor;
    }

    public void SelectTower(GameObject tower) {
        if (tower.GetComponent<SelectTowerPlacement>().scrapCost < ScrapEconomy.currentScrap || Manager.devMode == false) {
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
