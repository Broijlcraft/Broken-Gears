using UnityEngine.UI;
using UnityEngine;
using System;

namespace BrokenGears.Old {
    public class Dialogue : MonoBehaviour {
        public static Dialogue d_Single;

        [SerializeField] private Menu dialogueHolder;
        [SerializeField] private Button continueButton;
        [SerializeField] private Text continueButtonText, titleTextObject, dialogueTextObject;

        [Space, SerializeField] private GameOverSettings gameOverSettings;
        [Header("Tutorial"), SerializeField] private bool isTutorial, isDemo;
        [Space, SerializeField] private TutorialSettings tutorialSettings, demoSettings;
        private delegate void Listener();

        #region Get/Set
        public bool IsDemo() {
            return isDemo;
        }
        #endregion

        private void Awake() {
            d_Single = this;
        }

        private void Start() {
            if (isTutorial || isDemo) {
                TutorialSettings settings;

                if (isTutorial) {
                    settings = tutorialSettings;
                } else {
                    settings = demoSettings;
                }

                settings.NextTutorialIntroText();
                continueButton.onClick.RemoveAllListeners();
                continueButton.onClick.AddListener(settings.NextTutorialIntroText);
            }
        }

        public void CloseDialogue() {
            if (MenuManager.mm_Single.currentMenu == dialogueHolder) {
                MenuManager.mm_Single.CloseMenu();
            }
        }

        public void SetDialogue(GameOverSettings specificGameOverSettings, GameOverState gameOverState) {
            continueButton.onClick.RemoveAllListeners();
            SpecificGameOverDetails details;
            if (gameOverState == GameOverState.Failure) {
                details = specificGameOverSettings.lostSettings;
                if (!isDemo) {
                    continueButton.onClick.AddListener(() => DummyVoid("This button does nothing, all hope is lost anyways"));
                }
            } else {
                details = specificGameOverSettings.winSettings;
                if (!isDemo) {
                    continueButton.onClick.AddListener(() => DummyVoid("gg"));
                }
            }

            SetDialogue(details.gameOverTitle, details.gameOverText);
        }

        public void DummyVoid(string s) {
            dialogueTextObject.text = s;
        }

        public void SetDialogue(string newTitle, string newDialogue) {
            //Debug.LogWarning(this + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            if (MenuManager.mm_Single.currentMenuState == MenuState.Closed) {
                MenuManager.mm_Single.OpenMenu(dialogueHolder);
            }

            if (MenuManager.mm_Single.currentMenu == dialogueHolder) {
                titleTextObject.text = newTitle;
                dialogueTextObject.text = newDialogue;
            }
        }

        public void GameOverDialogue(GameOverState gameOverState) {
            if (isTutorial) {
                SetDialogue(tutorialSettings.tutorialGameOverSettings, gameOverState);
            } else if (isDemo) {
                SetDialogue(demoSettings.tutorialGameOverSettings, gameOverState);
            } else {
                SetDialogue(gameOverSettings, gameOverState);
            }
        }
    }

    [Serializable]
    public class TutorialSettings {
        public string dialogueTitle;
        [TextArea]
        public string[] tutorialIntroTexts;
        [Space]
        public GameOverSettings tutorialGameOverSettings;
        public int currentTextIndex;

        public void NextTutorialIntroText() {
            if (currentTextIndex < tutorialIntroTexts.Length) {
                Dialogue.d_Single.SetDialogue(dialogueTitle, tutorialIntroTexts[currentTextIndex]);
                currentTextIndex++;
            } else {
                Dialogue.d_Single.CloseDialogue();
                WaveSpawner.singleWS.StartSpawnSequence();
            }
        }
    }

    [Serializable]
    public class GameOverSettings {

        public SpecificGameOverDetails winSettings;
        public SpecificGameOverDetails lostSettings;

    }

    [Serializable]
    public class SpecificGameOverDetails {
        public string gameOverTitle;
        [TextArea]
        public string gameOverText;
    }
}