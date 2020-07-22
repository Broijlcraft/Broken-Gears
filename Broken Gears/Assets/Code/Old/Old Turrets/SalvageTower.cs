using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalvageTower : MonoBehaviour {

    public bool bought;
    public int price;

    public GameObject vfx;
    
    public void ActivateTower() {
        bought = true;
        OldTowerManager.activeScrapTower++;
        vfx.SetActive(true);
        OldUiManager.staticMenuScript.MenuSwitch("none");
        ScrapEconomy.RemoveScrap(price);
    }
}
