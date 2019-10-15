using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static Transform canvas;
    public static Transform mobileCanvas;
    public static ScrapEconomy scrapEconomy;
    public bool monitor;
    public static bool staticMonitor;
    public GameObject healthFab;
    public static GameObject healthSlider;
    public static Camera cam;
    public static bool devMode;
    public Text devText;
    public Transform t;
    public static Transform target;

    private void Awake() {
        target = t;
        cam = Camera.main;
        canvas = GameObject.Find("Canvas").transform;
        mobileCanvas = GameObject.Find("MobileCanvas").transform;
        scrapEconomy = GetComponent<ScrapEconomy>();
        staticMonitor = monitor;
        healthSlider = healthFab;
        devText = canvas.Find("DevText").GetComponentInChildren<Text>();
        ChangeMode();
    }

    private void Update() {
        if (Input.GetButtonDown("DevMode")) {
            ChangeMode();
        }
    }

    public void ChangeMode() {
        if (devMode == true) {
            devMode = false;
            devText.text = ("DevMode");
        } else {
            devMode = true;
            devText.text = ("");
        }
    }
}
