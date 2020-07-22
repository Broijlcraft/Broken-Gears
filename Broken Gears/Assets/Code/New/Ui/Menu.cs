using UnityEngine;

public class Menu : MonoBehaviour {

    public MenuManager.MenuState menuPosition;
    public bool canNotGoBackWithEsc, saveSettingsOnClose;
    public Menu previousMenu;

    public virtual void ExtraFunctionality() {}
}