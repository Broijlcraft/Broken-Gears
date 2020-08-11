using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Menu))]
public class StartScreen : MonoBehaviour {
    public Text textObject;
    [TextArea]
    public string[] introscreenTexts;
    [HideInInspector] public int currentIndex;

    private void Start() {
        MenuManager.mm_Single.OpenMenu(GetComponent<Menu>());
        NextText();
    }

    public void NextText() {
        if(currentIndex < introscreenTexts.Length) {
            textObject.text = introscreenTexts[currentIndex];
            currentIndex++;
        } else {
            MenuManager.mm_Single.CloseMenu();
        }
    }
}