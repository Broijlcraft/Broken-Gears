using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

    public List<GameObject> towerList = new List<GameObject>();
    public static GameObject selectedTower;
    public Vector4 canPlaceColor;
    public Vector4 canNotPlaceColor;
    public static Vector4 canPlace; 
    public static Vector4 canNotPlace;
    public static int activeScrapTower = 0;

    public Vector3 staticMinXRotation;
    public static Vector3 minXRotation;
    public Vector3 staticPlusXRotation;
    public static Vector3 plusXRotation;
    public Vector3 staticMinZRotation;
    public static Vector3 minZRotation;
    public Vector3 staticPlusZRotation;
    public static Vector3 plusZRotation;

    private void Awake() {
        canPlace = canPlaceColor;
        canNotPlace = canNotPlaceColor;
        minXRotation = staticMinXRotation;
        plusXRotation = staticPlusXRotation;
        minZRotation = staticMinZRotation;
        plusZRotation = staticPlusZRotation;
    }

    public void SelectTower(GameObject tower) {
        if (tower.GetComponent<SelectTowerPlacement>().scrapCost <= ScrapEconomy.currentScrap || Manager.devMode == false) {
            if (selectedTower != null) {
                Destroy(selectedTower);
            }
            selectedTower = Instantiate(tower);
            if (selectedTower.GetComponent<Turret>().turretImg != null) {
                GameObject turretImg = Instantiate(selectedTower.GetComponent<Turret>().turretImg, Vector3.zero, Quaternion.identity);
                turretImg.transform.SetParent(Manager.healthCanvas);
            }
        }
    }
}
