using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static Transform canvas;
    public static Transform healthCanvas;
    public static Transform scrapCanvas;
    public static ScrapEconomy scrapEconomy;
    public bool monitor;
    public static bool staticMonitor;
    public GameObject healthFab;
    public static GameObject healthSlider;
    public static Camera cam;
    public static bool devMode;
    public Text devText;

    private void Awake() {
        cam = Camera.main;
        canvas = GameObject.Find("Canvas").transform;
        if (SceneManager.GetActiveScene().name != "MainMenu") {
            healthCanvas = GameObject.Find("HealthCanvas").transform;
            scrapCanvas = GameObject.Find("ScrapCanvas").transform;
            scrapEconomy = GetComponent<ScrapEconomy>();
            staticMonitor = monitor;
            healthSlider = healthFab;
            devText = canvas.Find("DevText").GetComponentInChildren<Text>();
        }
    }

    private void Update() {
        if (Input.GetButtonDown("DevMode") && SceneManager.GetActiveScene().name != "MainMenu") {
            ChangeMode();
        }
    }

    public void ChangeMode() {
        if (devMode == true) {
            devMode = false;
            devText.text = ("");
        } else {
            devMode = true;
            devText.text = ("DevMode");
        }
    }
}
