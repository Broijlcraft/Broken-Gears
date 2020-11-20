using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuyTowerButton : UiElementHover {

    [SerializeField] private Tower tower;
    private Button button;

    #region Get/Set
    public Tower GetTower() {
        return tower;
    }
    public Button GetButton() {
        return button;
    }
    #endregion

    private void Awake() {
        button = GetComponent<Button>();
    }

    private void Start() {
        button.onClick.AddListener(() => TowerManager.singleTM.PickTower(tower));
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        if (tower && button.interactable) {
            TowerManager.singleTM.SetTowerNameAndValueOnHover(tower.GetTowerName(), tower.GetBuyScrapPrice().ToString());
        }
    }

    public override void OnPointerExit(PointerEventData eventData) {
        if (tower && button.interactable) {
            TowerManager.singleTM.SetTowerNameAndValueOnHover("", "");
        }
    }
}