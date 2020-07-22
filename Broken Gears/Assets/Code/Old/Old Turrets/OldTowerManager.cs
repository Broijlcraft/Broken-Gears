using System.Collections.Generic;
using UnityEngine;

public class OldTowerManager : MonoBehaviour {

    public static OldTowerManager old_tm_Single;

    public List<GameObject> towerList = new List<GameObject>();
    public static GameObject selectedTower;
    public Vector4 canPlaceColor, canNotPlaceColor;
    public static int activeScrapTower = 0;

    public Vector3 minXRotation, plusXRotation, minZRotation, plusZRotation;

    private void Awake() {
        old_tm_Single = this;
    }

    public void SelectTower(GameObject tower) {
        if (OldUiManager.staticMenuScript.menuState == MenuScript.MenuState.none) {
            if (tower.GetComponent<OldSelectTowerPlacement>().scrapCost <= ScrapEconomy.currentScrap || OldManager.devMode == true) {
                if (selectedTower != null) {
                    Destroy(selectedTower);
                }
                selectedTower = Instantiate(tower);
            }
        }
    }
}
