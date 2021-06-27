using UnityEngine.UI;
using UnityEngine;

namespace BrokenGears.Old {
    public class MenuButton : MonoBehaviour {

        public Menu nextMenu;
        Menu menu;
        [HideInInspector] public Button button;

        private void Awake() {
            menu = GetComponentInParent<Menu>();
            button = GetComponent<Button>();
            button.onClick.AddListener(OpenSpecificMenu);
        }

        public void ResetButtonToDefaultOnclick() {
            button.onClick.AddListener(OpenSpecificMenu);
        }

        public void OpenSpecificMenu() {
            MenuManager.mm_Single.OpenMenu(nextMenu);
            if (menu) {
                menu.gameObject.SetActive(false);
            }
        }
    }
}