using System.Collections.Generic;
using UnityEngine;

public class OldTowerManager : MonoBehaviour {

    public static OldTowerManager old_tm_Single;

    public List<GameObject> towerList = new List<GameObject>();
    public GameObject selectedTower;
    public Vector4 canPlaceColor, canNotPlaceColor;
    public int activeScrapTower = 0;

    public Vector3 minXRotation, plusXRotation, minZRotation, plusZRotation;

    private void Awake() {
        old_tm_Single = this;
    }

    public void SelectTower(GameObject tower) {
        //if (OldMenuScript.old_ms_Single.menuState == OldMenuScript.MenuState.none) {
        //    //if (tower.GetComponent<OldSelectTowerPlacement>().scrapCost <= OldScrapEconomy.old_se_Single.currentScrap || OldManager.old_m_Single.devMode == true) {
        //        if (selectedTower != null) {
        //            Destroy(selectedTower);
        //        }
        //        selectedTower = Instantiate(tower);
        //    //}
        //}
    }
}
