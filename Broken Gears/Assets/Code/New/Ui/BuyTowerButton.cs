using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuyTowerButton : UiElementHover {

    public Tower tower;
    [HideInInspector] public Button button;

    private void Awake() {    
        button = GetComponent<Button>();
    }

    private void Start() {
        button.onClick.AddListener(() => TowerManager.tm_Single.PickTower(tower));
    }
    
    public override void OnPointerEnter(PointerEventData eventData) {
        if (tower && button.interactable) {
            UiManager.um_single.SetTowerNameAndValue(tower.towerName, tower.buyScrapPrice.ToString());
        }
    }

    public override void OnPointerExit(PointerEventData eventData) {
        if (tower && button.interactable) {
            UiManager.um_single.SetTowerNameAndValue("", "");
        }
    }
}