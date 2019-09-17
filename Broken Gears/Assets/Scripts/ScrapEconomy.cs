﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScrapEconomy : MonoBehaviour {

    public static Sprite scrap;
    public int currentScrapValue;
    public string scrapText;

    public Text uiScrap;

    private void Start() {
        uiScrap = Manager.canvas.Find("HUD").Find("Scrap").GetComponentInChildren<Text>();
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
