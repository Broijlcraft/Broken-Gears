using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
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

    GameObject cameraControl;

    UiManager uiManager;
    Movement movement;
    ZoomScript zoomAndSelectTile;
    PlayerLook playerLook;
    XmlManager xmlManager;

    public enum MenuState { 
        none,
        mainMenu,
        options,
        video,
        audio
    }

    public MenuState menuState;

    private void Awake() {
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        SetItActive(uiManager.menus[1], uiManager.menus[2]);

        cameraControl = GameObject.Find("CamControl");
        zoomAndSelectTile = cameraControl.GetComponentInChildren<ZoomScript>();
        playerLook = cameraControl.GetComponentInChildren<PlayerLook>();
        movement = cameraControl.GetComponent<Movement>();
        xmlManager = GameObject.Find("GameManager").GetComponent<XmlManager>();

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

        ////camSensitivity.wholeNumbers = true;
        ////camSensitivity.onValueChanged.AddListener(CamSenseChanged);
        ////zoomSensitivity.onValueChanged.AddListener(ZoomSenseChanged);
        volume.onValueChanged.AddListener(VolumeChanged);
        sfx.onValueChanged.AddListener(SFXSenseChanged);
        music.onValueChanged.AddListener(MusicSenseChanged);

        //SetSliderRange(camSensitivity, maxCamSense, minCamSense);
        //SetSliderRange(zoomSensitivity, maxZoomSense, minZoomSense);
        SetSliderRange(volume, maxVolume, minVolume);
        SetSliderRange(sfx, maxSFX, minSFX);
        SetSliderRange(music, maxMusic, minMusic);

        SetItActive(null, null);
    }

    public void MenuSwitch(string s) {
        try {
            menuState = (MenuState)Enum.Parse(typeof(MenuState), s);
            print("In here");
        } catch (Exception ex) {
            print("not in here");
            print(ex);
        }
        //menuState = (MenuState)i;
        switch (menuState) {
            case MenuState.none:
            //menuState = MenuState.none;
            SetItActive(null, null);
            break;
            case MenuState.mainMenu: case MenuState.options:
            //menuState = MenuState.mainMenu;
            SetItActive(uiManager.menus[0], uiManager.menus[(int)menuState]);
            break;
            case MenuState.audio: case MenuState.video:
            //menuState = MenuState.options;
            SetItActive(uiManager.menus[0], uiManager.menus[(int)menuState]);
            break;
        }
    }

    void SetItActive(GameObject a, GameObject b) {
        for (int i = 0; i < uiManager.menus.Count; i++) {
            if (uiManager.menus[i] != null) {
                uiManager.menus[i].SetActive(false);
            }      
        }

        if (a != null) {
            a.SetActive(true);
            print(a.activeSelf);
        }

        if (b != null) {
            b.SetActive(true);
        }
    }

    private void Update() {
        if (Input.GetButtonDown("Cancel")) {
            int ib = (int)menuState;
            switch (ib) {
                case 0: case 2:
                menuState = MenuState.mainMenu;
                break;
                case 3: case 4:
                menuState = MenuState.options;
                break;
            }
            //MenuSwitch();
        }
    }

    void SetSliderRange(Slider slider, float max, float min) {
        if (slider != null) {
            slider.maxValue = max - min;
            slider.value = slider.maxValue / 2;
        }
    }

    public void OldMenuSwitch() {
        //if (uiManager.menusHolder.activeSelf == false) {
        //    uiManager.menusHolder.SetActive(true);
        //    uiManager.mainMenu.SetActive(true);
        //    uiManager.optionsMenu.SetActive(false);
        //    Time.timeScale = 0;
        //} else {
        //    if (uiManager.mainMenu.activeSelf == false) {
        //        uiManager.optionsMenu.SetActive(false);
        //        uiManager.mainMenu.SetActive(true);
        //    } else {
        //        uiManager.menusHolder.SetActive(false);
        //        Time.timeScale = 1;
        //    }
        //}
    }

    public void QuitGame() {
        xmlManager.Save();
        Application.Quit();
    }

    public void ResetToDefault() {
        for (int i = 0; i < uiManager.sliders.Count; i++) {
            ResetSlider(uiManager.sliders[i].GetComponentInChildren<Slider>());
        }
        xmlManager.Save();
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
