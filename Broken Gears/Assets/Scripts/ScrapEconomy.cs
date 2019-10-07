using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScrapEconomy : MonoBehaviour {

    public GameObject scrapFab;
    public int startScrap;
    public static int currentScrapValue;
    public string scrapText;

    //public int scrapAddOnSalvage;

    public Text uiScrap;

    private void Start() {
        uiScrap = Manager.canvas.Find("HUD").Find("Scrap").GetComponentInChildren<Text>();
        currentScrapValue = startScrap;
        ScrapUpdate();
    }

    public void AddScrap(int i) {
        currentScrapValue += i;
        ScrapUpdate();
    }

    public void RemoveScrap(int i) {
        currentScrapValue -= i;
        ScrapUpdate();
    }

    void ScrapUpdate() {
        uiScrap.text = (scrapText + " " + currentScrapValue);
    }
}
