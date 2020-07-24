using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class OldManager : MonoBehaviour {

    public static OldManager old_m_Single;

    public Transform canvasTest, healthCanvas, scrapCanvas;
    public bool monitor, devMode;
    public GameObject healthFab;
    public Camera cam;
    public Text devText;

    private void Awake() {
        old_m_Single = this;
    }

    private void Start() {
        cam = Camera.main;
        if (SceneManager.GetActiveScene().name != "MainMenu") {
            healthCanvas = GameObject.Find("HealthCanvas").transform;
            scrapCanvas = GameObject.Find("ScrapCanvas").transform;
            devText = canvasTest.Find("DevText").GetComponentInChildren<Text>();
            devMode = true;
            ChangeMode();
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
