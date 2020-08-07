using UnityEngine.UI;
using UnityEngine;

public class UiManager : MonoBehaviour {
    public static UiManager um_single;
    public Text towerNameTextForPurchase, towerValueTextForPurchase;
    public BuyTowerButton[] buyTowerButtons;

    private void Awake() {
        um_single = this;
        SetTowerNameAndValue("", "");
    }

    private void Start() {
        CheckPricesSetInteractableAndNot();
    }

    public void SetTowerNameAndValue(string towerName, string value) {
        towerNameTextForPurchase.text = towerName;
        towerValueTextForPurchase.text = value;
    }

    public void CheckPricesSetInteractableAndNot() {
        for (int i = 0; i < buyTowerButtons.Length; i++) {
            if(buyTowerButtons[i] && buyTowerButtons[i].tower) {
                if(buyTowerButtons[i].tower.buyScrapPrice <= ScrapManager.sm_single.currentScrap) {
                    buyTowerButtons[i].button.interactable = true;
                } else {
                    buyTowerButtons[i].button.interactable = false;
                }
            } else {
                buyTowerButtons[i].button.interactable = false;
            }
        }
    }
}