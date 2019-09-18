using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static Transform canvas;
    public static Transform mobileCanvas;
    public static ScrapEconomy scrapEconomy;
    public bool monitor;
    public static bool staticMonitor;

    private void Awake() {
        canvas = GameObject.Find("Canvas").transform;
        mobileCanvas = GameObject.Find("MobileCanvas").transform;
        scrapEconomy = GetComponent<ScrapEconomy>();
        staticMonitor = monitor;
    }
}
