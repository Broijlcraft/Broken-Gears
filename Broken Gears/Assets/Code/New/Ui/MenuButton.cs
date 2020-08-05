﻿using UnityEngine.UI;
using UnityEngine;

public class MenuButton : MonoBehaviour {

    public Menu nextMenu;
    Menu menu;

    private void Awake() {
        GetComponent<Button>().onClick.AddListener(OpenSpecificMenu);
        menu = GetComponentInParent<Menu>();
    }

    public void OpenSpecificMenu() {
        MenuManager.mm_Single.OpenMenu(nextMenu, false);
        if (menu) {
            menu.gameObject.SetActive(false);
        }
    }
}