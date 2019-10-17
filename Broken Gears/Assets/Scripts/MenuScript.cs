using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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

    //public GameObject menusHolder;
    //public GameObject menu;
    //public GameObject optionsMenu;

    GameObject cameraControl;

    UiManager uiManager;

    Movement movement;
    ZoomScript zoomAndSelectTile;
    PlayerLook playerLook;
    XmlManager xmlManager;
    public enum MenuState {
        main,
        options,
        video,
        audio
    }

    public MenuState menuState;

    private void Awake() {
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        uiManager.menusHolder.SetActive(true);
        uiManager.optionsMenu.SetActive(true);

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
                print("name = " + uiManager.sliders[i].name);
            }
            if (uiManager.sliders[i].name == "SFX") {
                sfx = uiManager.sliders[i].GetComponentInChildren<Slider>();
                print("name = " + uiManager.sliders[i].name);
            }
            if (uiManager.sliders[i].name == "Music") {
                music = uiManager.sliders[i].GetComponentInChildren<Slider>();
                print("name = " + uiManager.sliders[i].name);
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

        uiManager.menusHolder.SetActive(false);
        uiManager.optionsMenu.SetActive(false);
    }

    private void Update() {
        if (Input.GetButtonDown("Cancel")) {
            MenuSwitch();
        }
    }

    void SetSliderRange(Slider slider, float max, float min) {
        if (slider != null) {
            slider.maxValue = max - min;
            slider.value = slider.maxValue / 2;
        }
    }

    public void MenuSwitch() {
        //xmlManager.Save();
        if (uiManager.menusHolder.activeSelf == false) {
            uiManager.menusHolder.SetActive(true);
            uiManager.menu.SetActive(true);
            uiManager.optionsMenu.SetActive(false);
            Time.timeScale = 0;
        } else {
            if (uiManager.menu.activeSelf == false) {
                uiManager.optionsMenu.SetActive(false);
                uiManager.menu.SetActive(true);
            } else {
                uiManager.menusHolder.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void QuitGame() {
        //xmlManager.Save();
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
