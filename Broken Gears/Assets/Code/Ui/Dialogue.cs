using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Dialogue : MonoBehaviour {
    public static Dialogue d_Single;
    public Menu dialogueHolder;
    public Text titleTextObject, dialogueTextObject;
    public Button continueButton;
    [Space]
    public GameOverSettings gameOverSettings;
    [Header("Tutorial")]
    public bool isTutorial;
    [Space]
    public TutorialSettings tutorialSettings;

    private void Awake() {
        d_Single = this;
    }

    private void Start() {
        if (isTutorial && !GameManager.gm_Single.devMode) {
            WaveSpawner.ws_Single.waveFunctionality = false;
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(tutorialSettings.NextTutorialIntroText);
            tutorialSettings.NextTutorialIntroText();
        }
    }

    public void CloseDialogue() {
        if (MenuManager.mm_Single.currentMenu == dialogueHolder) {
            MenuManager.mm_Single.CloseMenu();
        }
    }

    public void SetDialogue(GameOverSettings specificGameOverSettings, GameManager.GameOverState gameOverState) {
        continueButton.onClick.RemoveAllListeners();
        SpecificGameOverDetails details;
        if(gameOverState == GameManager.GameOverState.Failure) {
            details = specificGameOverSettings.lostSettings;
            continueButton.onClick.AddListener(() => DummyVoid("This button does nothing, all hope is lost anyways"));
        } else {
            details = specificGameOverSettings.winSettings;
            continueButton.onClick.AddListener(() => DummyVoid("gg"));
        }
        SetDialogue(details.gameOverTitle, details.gameOverText);
    }

    public void DummyVoid(string s) {
        dialogueTextObject.text = s;
    }

    public void SetDialogue(string newTitle, string newDialogue) {
        if(MenuManager.mm_Single.currentMenuState == MenuManager.MenuState.Closed) {
            MenuManager.mm_Single.OpenMenu(dialogueHolder);
        }

        if(MenuManager.mm_Single.currentMenu == dialogueHolder) {
            titleTextObject.text = newTitle;
            dialogueTextObject.text = newDialogue;
        }
    }

    public void GameOverDialogue(GameManager.GameOverState gameOverState) {
        if (isTutorial) {
            SetDialogue(tutorialSettings.tutorialGameOverSettings, gameOverState);
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
    int currentTextIndex;

    public void NextTutorialIntroText() {
        if(currentTextIndex < tutorialIntroTexts.Length) {
            Dialogue.d_Single.SetDialogue(dialogueTitle, tutorialIntroTexts[currentTextIndex]);
            currentTextIndex++;
        } else {
            Dialogue.d_Single.CloseDialogue();
            WaveSpawner.ws_Single.waveFunctionality = true;
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