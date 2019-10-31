﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    [Header("Settings")]

    public Slider camSensitivity;
    public float maxCamSense;
    public float minCamSense;
    public Slider zoomSensitivity;
    public float maxZoomSense;
    public float minZoomSense; 
    public Slider volume;
    public float maxVolume = 1f;
    public float minVolume; 
    public Slider sfx;
    public float maxSFX = 1f;
    public float minSFX; 
    public Slider music;
    public float maxMusic = 1f;
    public float minMusic;

    [Header("Tower Interaction")]

    public Button buySellText;

    GameObject cameraControl;

    UiManager uiManager;
    Movement movement;
    ZoomScript zoomAndSelectTile;
    PlayerLook playerLook;
    //XmlManager xmlManager;

    string sceny;

    public enum MenuState { 
        none,
        mainMenu,
        options,
        controls,
        audio,
        towerInteraction
    }

    public MenuState menuState;

    private void Awake() {
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        if (SceneManager.GetActiveScene().name != "MainMenu") {
            SetItActive(uiManager.menus[1], uiManager.menus[2]);
            cameraControl = GameObject.Find("CamControl");
            zoomAndSelectTile = cameraControl.GetComponentInChildren<ZoomScript>();
            playerLook = cameraControl.GetComponentInChildren<PlayerLook>();
            movement = cameraControl.GetComponent<Movement>();

            for (int i = 0; i < uiManager.sliders.Count; i++) {
                if (uiManager.sliders[i].name == "CamSense") {
                    camSensitivity = uiManager.sliders[i].GetComponentInChildren<Slider>();
                }
                if (uiManager.sliders[i].name == "ZoomSense") {
                    zoomSensitivity = uiManager.sliders[i].GetComponentInChildren<Slider>();
                }
                if (uiManager.sliders[i].name == "MasterVolume") {
                    volume = uiManager.sliders[i].GetComponentInChildren<Slider>();
                }
                if (uiManager.sliders[i].name == "SFX") {
                    sfx = uiManager.sliders[i].GetComponentInChildren<Slider>();
                }
                if (uiManager.sliders[i].name == "Music") {
                    music = uiManager.sliders[i].GetComponentInChildren<Slider>();
                }
            }

            camSensitivity.wholeNumbers = true;
            camSensitivity.onValueChanged.AddListener(CamSenseChanged);
            zoomSensitivity.onValueChanged.AddListener(ZoomSenseChanged);
            volume.onValueChanged.AddListener(VolumeChanged);
            sfx.onValueChanged.AddListener(SFXSenseChanged);
            music.onValueChanged.AddListener(MusicSenseChanged);

            SetSliderRange(camSensitivity, maxCamSense, minCamSense);
            SetSliderRange(zoomSensitivity, maxZoomSense, minZoomSense);
            SetSliderRange(volume, maxVolume, minVolume);
            SetSliderRange(sfx, maxSFX, minSFX);
            SetSliderRange(music, maxMusic, minMusic);
            SetItActive(null, null);
        }
    }

    private void Start() {
        StartCam();
    }

    private void Update() {
        if (Input.GetButtonDown("Cancel") && SceneManager.GetActiveScene().name != "MainMenu") {
            int ib = (int)menuState;
            switch (ib) {
                case 1: case 5:
                menuState = MenuState.none;
                SetItActive(null, null);
                break;
                case 0: case 2:
                menuState = MenuState.mainMenu;
                SetItActive(uiManager.menus[0], uiManager.menus[(int)menuState]);
                break;
                case 3: case 4:
                menuState = MenuState.options;
                SetItActive(uiManager.menus[0], uiManager.menus[(int)menuState]);
                break;
            }
        }
    }

    public void StartCam() {
        InvokeRepeating("Move", playerLook.initialMoveDelay, 0);
    }

    void Move() {
        playerLook.moving = true;
        print("Move");
    }

    public void MenuSwitch(string s) {
        try {
            menuState = (MenuState)Enum.Parse(typeof(MenuState), s);
        } catch (Exception ex) {
            print("not in here");
            print(ex);
        }
        switch (menuState) {
            case MenuState.none:
            SetItActive(null, null);
            break;
            case MenuState.mainMenu: case MenuState.options:
            SetItActive(uiManager.menus[0], uiManager.menus[(int)menuState]);
            break;
            case MenuState.audio: case MenuState.controls:
            SetItActive(uiManager.menus[0], uiManager.menus[(int)menuState]);
            break;
            case MenuState.towerInteraction:
            SetItActive(uiManager.menus[(int)menuState], null);
            break;
        }
    }
    
    void SetItActive(GameObject a, GameObject b) {
        if (uiManager.menus.Count > 0) {
            for (int i = 0; i < uiManager.menus.Count; i++) {
                if (uiManager.menus[i] != null) {
                    uiManager.menus[i].SetActive(false);
                }      
            }

            if (a != null) {
                a.SetActive(true);
            }

            if (b != null) {
                b.SetActive(true);
            }
            PauseCheck();
        }
    }

    void PauseCheck() {
        if (menuState != MenuState.none && menuState != MenuState.towerInteraction) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    void SetSliderRange(Slider slider, float max, float min) {
        if (slider != null) {
            slider.maxValue = max - min;
            slider.value = slider.maxValue / 2;
        }
    }

    public void ChangeScene(string sceneName) {
        //start animation
        sceny = sceneName;
        Time.timeScale = 1;
        uiManager.fadePic.GetComponent<Animator>().SetTrigger("FadeOut");
        InvokeRepeating("StartScene", 1, 0);
    }

    void StartScene() {
        SceneManager.LoadScene(sceny);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceny));
    }

    public void QuitGame() {
        //xmlManager.Save();
        Application.Quit();
    }

    void ResetSlider(Slider slider) {
        slider.value = slider.maxValue / 2;
    }

    void CamSenseChanged(float value) {
        movement.rotationSpeed = value + minCamSense;
        playerLook.UpdateLookValue();
    }

    void ZoomSenseChanged(float value) {
        zoomAndSelectTile.zoomIncrease = value + minZoomSense;
    }

    void VolumeChanged(float value) {

    }

    void SFXSenseChanged(float value) {

    }

    void MusicSenseChanged(float value) {

    }
}
