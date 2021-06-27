using UnityEngine;

namespace BrokenGears.Old {
    public class GameManager : MonoBehaviour {
        public static GameManager gm_Single;

        private bool devMode = true;
        [SerializeField] private GameObject devmodeText;

        private bool gameIsOver;
        private GameOverState gameOverState;

        #region Get/Set
        public bool DevMode() {
            return devMode;
        }

        public bool GetGameIsOver() {
            return gameIsOver;
        }
        #endregion

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

    public enum GameOverState {
        Success,
        Failure
    }
}