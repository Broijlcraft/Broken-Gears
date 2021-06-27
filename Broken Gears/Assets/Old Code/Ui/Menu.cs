using UnityEngine;

namespace BrokenGears.Old {
    public class Menu : MonoBehaviour {

        public MenuState menuPosition;
        public bool canNotGoBackWithEsc, saveSettingsOnClose, dontOpenPreviousMenuOnJustMenuClose;
        public Menu previousMenu;

        public virtual void ExtraFunctionalityOnCLose() { }
        public virtual void ExtraFunctionalityOnOpen() { }
    }
}