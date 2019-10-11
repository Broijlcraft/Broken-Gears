using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScrapEconomy : MonoBehaviour {

    public GameObject scrapFab;
    public int startScrap;
    public static int currentScrap;
    public string scrapText;

    //public int scrapAddOnSalvage;

    public Text uiScrap;

    private void Start() {
        uiScrap = Manager.canvas.Find("HUD").Find("Scrap").GetComponentInChildren<Text>();
        currentScrap = startScrap;
        ScrapUpdate();
    }

    public void AddScrap(int i) {
        currentScrap += i;
        ScrapUpdate();
    }

    public void RemoveScrap(int i) {
        currentScrap -= i;
        ScrapUpdate();
    }

    void ScrapUpdate() {
        uiScrap.text = (scrapText + " " + currentScrap);
    }
}
