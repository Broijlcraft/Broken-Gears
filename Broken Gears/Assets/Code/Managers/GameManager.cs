using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager gm_Single;
    public bool devMode = true;
    public GameObject devmodeText;

    public enum GameOverState {
        Success,
        Failure
    }

    [HideInInspector] public bool gameIsOver;
    [HideInInspector] public GameOverState gameOverState;

    private void Awake() {
        gm_Single = this;
        DevModeTextOnOff();
    }

    private void Update() {
        if (Input.GetButtonDown("DevMode")) {
            devMode = !devMode;
            DevModeTextOnOff();
            TowerManager.singleTM.CheckPricesSetInteractableAndNot();
        }
    }

    public void SetGameOver(GameOverState gameOverState) {
        if (!gameIsOver) {
            this.gameOverState = gameOverState;
            gameIsOver = true;
            Dialogue.d_Single.GameOverDialogue(gameOverState);
        }
    }

    void DevModeTextOnOff() {
        if (devmodeText) {
            devmodeText.SetActive(devMode);
        }
    }
}