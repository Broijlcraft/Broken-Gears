﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public static MenuManager mm_Single;
    public Menu currentMenu;

    public enum MenuState {
        Closed,
        FirstPanel,
        DeeperPanel
    }

    public MenuState currentMenuState;

    [Space, Header("EscapeMenu")]
    public GameObject menuHolder;
    public Menu firstMenu;

    private void Awake() {
        mm_Single = this;
    }

    private void Update() {
        if (Input.GetButtonDown("Cancel")) {
            MoveUpOrCloseMenu();
        }
    }

    public void MoveUpOrCloseMenu() {

        if (currentMenuState == MenuState.Closed) {
            Time.timeScale = 0;
            menuHolder.SetActive(true);
            OpenMenu(firstMenu);
        } else {
            if (currentMenu && !currentMenu.canNotGoBackWithEsc) {
                if (currentMenu.menuPosition == MenuState.FirstPanel) {
                    menuHolder.SetActive(false);
                    firstMenu.gameObject.SetActive(false);
                    currentMenu = null;
                    currentMenuState = MenuState.Closed;
                    Time.timeScale = 1;
                } else {
                    currentMenu.gameObject.SetActive(false);
                    OpenMenu(currentMenu.previousMenu);
                }
            }
        }
    }

    public void OpenMenu(Menu menu) {
        currentMenu = menu;
        currentMenu.gameObject.SetActive(true);
        currentMenuState = menu.menuPosition;
    }

    public void BackToMenu() {

    }

    public void QuitGame() {
        Application.Quit();
    }
}