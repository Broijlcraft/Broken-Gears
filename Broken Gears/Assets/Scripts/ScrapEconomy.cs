using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScrapEconomy : MonoBehaviour {

    public GameObject scrapFab;
    public int startScrap;
    public static int currentScrap;
    public string scrapText;
    public static string staticScrapText;

    public static Text uiScrap;

    private void Start() {
        uiScrap = Manager.canvas.Find("HUD").Find("Scrap").GetComponentInChildren<Text>();
        currentScrap = startScrap;
        staticScrapText = scrapText;
        ScrapUpdate();
    }

    public static void AddScrap(int i) {
        currentScrap += i;
        ScrapUpdate();
    }

    public static void RemoveScrap(int i) {
        currentScrap -= i;
        ScrapUpdate();
    }

    public static void ScrapUpdate() {
        uiScrap.text = (staticScrapText + " " + currentScrap);
    }
}
