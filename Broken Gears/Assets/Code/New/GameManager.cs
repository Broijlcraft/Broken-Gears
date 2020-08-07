using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager gm_Single;
    public bool devMode = true;
    public GameObject devmodeText;

    private void Awake() {
        gm_Single = this;
        devmodeText.SetActive(devMode);
    }

    private void Update() {
        if (Input.GetButtonDown("DevMode")) {
            devMode = !devMode;
            devmodeText.SetActive(devMode);
        }
    }
}