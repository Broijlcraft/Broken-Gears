﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalvageTower : MonoBehaviour {

    public bool bought;
    public int price;

    public GameObject vfx;
    
    public void ActivateTower() {
        bought = true;
        TowerManager.activeScrapTower++;
        vfx.SetActive(true);
        UiManager.staticMenuScript.MenuSwitch("none");
        ScrapEconomy.RemoveScrap(price);
    }
}
