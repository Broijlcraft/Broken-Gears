using UnityEngine;

public class MenuManager : MonoBehaviour {

    public static MenuManager mm_Single;

    public enum MenuState {
        Closed,
        FirstPanel,
        DeeperPanel
    }

    [HideInInspector] public Menu currentMenu;
    /*[HideInInspector]*/ public MenuState currentMenuState;

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
            if (menuHolder && firstMenu) {
                menuHolder.SetActive(true);
                OpenMenu(firstMenu);
            }
        } else {
            if (currentMenu && !currentMenu.canNotGoBackWithEsc) {
                CloseMenu();
            }
        }
    }

    public void CloseMenu() {
        if (currentMenu) {
            currentMenu.ExtraFunctionalityOnCLose();
            currentMenu.gameObject.SetActive(false);
            if (currentMenu.menuPosition == MenuState.FirstPanel || currentMenu.dontOpenPreviousMenuOnJustMenuClose) {
                menuHolder.SetActive(false);
                currentMenu = null;
                currentMenuState = MenuState.Closed;
                Movement.m_Single.canMove = true;
                Time.timeScale = 1;
            } else {
                OpenMenu(currentMenu.previousMenu);
            }
        }
    }

    public void OpenMenu(Menu menu) {
        if (menu) {
            menu.ExtraFunctionalityOnOpen();
            currentMenu = menu;
            currentMenu.gameObject.SetActive(true);
            currentMenuState = menu.menuPosition;
            Movement.m_Single.canMove = false;
        }
    }

    public void BackToMainMenu() {

    }

    public void QuitGame() {
        Application.Quit();
    }
}