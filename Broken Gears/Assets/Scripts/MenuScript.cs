using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuScript : MonoBehaviour {
    public Slider camSensitivity;
    public float maxCamSense = 300f;
    public float minCamSense = 30f;
    public Slider zoomSensitivity;
    public float maxZoomSense = 5f;
    public float minZoomSense = 1f; 
    public Slider volume;
    public float maxVolume = 1f;
    public float minVolume; 
    public Slider sfx;
    public float maxSFX = 1f;
    public float minSFX; 
    public Slider music;
    public float maxMusic = 1f;
    public float minMusic;

    public GameObject menusHolder;
    public GameObject menu;
    public GameObject optionsMenu;

    GameObject[] slid;

    private void Start() {
        menusHolder.SetActive(true);
        optionsMenu.SetActive(true);
        
        slid = GameObject.FindGameObjectsWithTag("Slider");

        for (int i = 0; i < slid.Length; i++) {
            if (slid[i].name == "CamSense") {
                camSensitivity = slid[i].GetComponentInChildren<Slider>();
            }
            if (slid[i].name == "ZoomSense") {
                zoomSensitivity = slid[i].GetComponentInChildren<Slider>();
            }
            if (slid[i].name == "Volume") {
                volume = slid[i].GetComponentInChildren<Slider>();
            }
            if (slid[i].name == "SFX") {
                sfx = slid[i].GetComponentInChildren<Slider>();
            }
            if (slid[i].name == "Music") {
                music = slid[i].GetComponentInChildren<Slider>();
            }
        }

        camSensitivity.onValueChanged.AddListener(CamSenseChanged); 
        SetSliderValues(camSensitivity, maxCamSense, minCamSense);
        zoomSensitivity.onValueChanged.AddListener(ZoomSenseChanged);
        SetSliderValues(zoomSensitivity, maxZoomSense, minZoomSense);
        volume.onValueChanged.AddListener(VolumeChanged);
        SetSliderValues(volume, maxVolume, minVolume);
        sfx.onValueChanged.AddListener(SFXSenseChanged);
        SetSliderValues(sfx, maxSFX, minSFX);
        music.onValueChanged.AddListener(MusicSenseChanged);
        SetSliderValues(music, maxMusic, minMusic);

        menusHolder.SetActive(false);
        optionsMenu.SetActive(false);
    }

    private void Update() {
        if (Input.GetButtonDown("Cancel")) {
            MenuSwitch();
        }
    }

    void SetSliderValues(Slider slider, float max, float min) {
        if (slider != null) {
            //if xml doesn't have value
            slider.maxValue = max - min;
            slider.value = slider.maxValue / 2;
        }
    }

    void MenuSwitch() {
        if (menusHolder.activeSelf == false) {
            menusHolder.SetActive(true);
            menu.SetActive(true);
            optionsMenu.SetActive(false);
            Time.timeScale = 0;
        } else {
            if (menu.activeSelf == false) {
                optionsMenu.SetActive(false);
                menu.SetActive(true);
            } else {
                menusHolder.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void ResetToDefault() {
        for (int i = 0; i < slid.Length; i++) {
            ResetSlider(slid[i].GetComponentInChildren<Slider>());
        }
    }

    void ResetSlider(Slider slider) {
        slider.value = slider.maxValue / 2;
    }

    void CamSenseChanged(float value) {

    }

    void ZoomSenseChanged(float value) {

    }

    void VolumeChanged(float value) {

    }

    void SFXSenseChanged(float value) {

    }

    void MusicSenseChanged(float value) {

    }
}
