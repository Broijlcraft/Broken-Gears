using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager gm_Single;
    public bool devMode = true;
    public GameObject devmodeText;

    [Header("HideInInspector")] public bool gameIsOver;

    public enum GameOverState {
        Success,
        Failure
    }

    public GameOverState gameOverState;

    private void Awake() {
        gm_Single = this;
        DevModeTextOnOff();
    }

    private void Update() {
        if (Input.GetButtonDown("DevMode")) {
            devMode = !devMode;
            DevModeTextOnOff();
            TowerManager.tm_Single.CheckPricesSetInteractableAndNot();
        }
    }

    public void SetGameOver(GameOverState gameOverState) {
        if (!gameIsOver) {
            this.gameOverState = gameOverState;
            gameIsOver = true;
        }
    }

    void DevModeTextOnOff() {
        if (devmodeText) {
            devmodeText.SetActive(devMode);
        }
    }
}