using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OldMenuScript : MonoBehaviour {

    public static OldMenuScript old_ms_Single;

    public bool dev, newDev;
    [Header("Settings")]

    public Slider camSensitivity;
    public float maxCamSense, minCamSense;
    public Slider zoomSensitivity;
    public float maxZoomSense, minZoomSense; 
    public Slider volume;
    public float maxVolume = 1f, minVolume; 
    public Slider sfx;
    public float maxSFX = 1f, minSFX; 
    public Slider music;
    public float maxMusic = 1f, minMusic;

    [Header("Tower Interaction")]

    public Button buySellText;
    
    public AudioSource audio;

    GameObject industrialLight;
    bool alarm;
    float alarmTime;
    int alarmAmount;
    public int maxAlarmAmount;

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
        old_ms_Single = this;
        if (newDev) {
            if (SceneManager.GetActiveScene().name != "MainMenu") {
                //SetItActive(OldUiManager.old_um_Single.menus[1], OldUiManager.old_um_Single.menus[2]);
                industrialLight = GameObject.Find("FactoryLight");
                if (industrialLight != null) {
                    audio = industrialLight.GetComponent<AudioSource>();
                }
            }
        }
    }

    private void Update() {
        if (alarm == true) {
            if (alarmTime < audio.clip.length) {
                alarmTime += Time.deltaTime;
            } else {
                alarmTime = 0;
                alarmAmount++;
                if (alarmAmount == maxAlarmAmount) {
                    audio.Stop();
                    alarm = false;
                }
            }
        }
        if (Input.GetButtonDown("Cancel") && SceneManager.GetActiveScene().name != "MainMenu" /*&& OldUiManager.old_um_Single.gameOver == false */ && !dev) {
            int ib = (int)menuState;
            switch (ib) {
                case 1: case 5:
                menuState = MenuState.none;
                SetItActive(null, null);
                break;
                case 0: case 2:
                menuState = MenuState.mainMenu;
                //SetItActive(OldUiManager.old_um_Single.menus[0], OldUiManager.old_um_Single.menus[(int)menuState]);
                break;
                case 3: case 4:
                menuState = MenuState.options;
                //SetItActive(OldUiManager.old_um_Single.menus[0], OldUiManager.old_um_Single.menus[(int)menuState]);
                break;
            }
        }
    }

    public void StartCam() {
        industrialLight.GetComponentInChildren<Animator>().SetBool("Flashing", true);
        audio.Play();
        alarm = true;
        InvokeRepeating("Move", OldPlayerLook.old_pl_single.initialMoveDelay, 0);
    }

    void Move() {
        OldPlayerLook.old_pl_single.moving = true;
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
            //SetItActive(OldUiManager.old_um_Single.menus[0], OldUiManager.old_um_Single.menus[(int)menuState]);
            break;
            case MenuState.audio: case MenuState.controls:
            //SetItActive(OldUiManager.old_um_Single.menus[0], OldUiManager.old_um_Single.menus[(int)menuState]);
            break;
            case MenuState.towerInteraction:
            //SetItActive(OldUiManager.old_um_Single.menus[(int)menuState], null);
            break;
        }
    }
    
    void SetItActive(GameObject a, GameObject b) {
        if (/*OldUiManager.old_um_Single.menus.Count > 0 &&*/ !dev) {
            //for (int i = 0; i < OldUiManager.old_um_Single.menus.Count; i++) {
            //    if (OldUiManager.old_um_Single.menus[i] != null) {
            //        OldUiManager.old_um_Single.menus[i].SetActive(false);
            //    }      
            //}

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
        sceny = sceneName;
        Time.timeScale = 1;
        //OldUiManager.old_um_Single.fadePic.GetComponent<Animator>().SetTrigger("FadeOut");
        InvokeRepeating("StartScene", 1, 0);
    }

    void StartScene() {
        SceneManager.LoadScene(sceny);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceny));
    }
}
