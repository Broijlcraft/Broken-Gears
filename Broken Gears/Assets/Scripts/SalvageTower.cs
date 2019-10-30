using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalvageTower : MonoBehaviour {

    public bool bought;
    public int price;

    public Vector4 v;
    public Renderer matRenderer;
    public GameObject vfx;

    private void Start() {    
        matRenderer.material.color = new Color(v.x, v.y, v.z, v.w);        
    }

    public void ActivateTower() {
        bought = true;
        TowerManager.activeScrapTower++;
        matRenderer.material.color = Color.white;
        vfx.SetActive(true);
        UiManager.staticMenuScript.MenuSwitch("none");
        ScrapEconomy.RemoveScrap(price);
    }
}
