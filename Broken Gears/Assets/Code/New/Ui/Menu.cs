using UnityEngine;

public class Menu : MonoBehaviour {

    public MenuManager.MenuState menuPosition;
    public bool canNotGoBackWithEsc, saveSettingsOnClose, dontOpenPreviousMenuOnJustMenuClose;
    public Menu previousMenu;

    public virtual void ExtraFunctionalityOnCLose() {}
    public virtual void ExtraFunctionalityOnOpen() {}
}