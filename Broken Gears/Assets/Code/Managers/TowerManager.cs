﻿using UnityEngine.UI;
using UnityEngine;

public class TowerManager : MonoBehaviour {
    public static TowerManager tm_Single;
    public LayerMask layersToIgnoreWhenAttacking, tileMask;
    public string buildableTileTag = "BuildableTile", towerTag = "Tower";
    public Color canPlaceColor, canNotPlaceColor;

    public TowerRotations towerRotations;
    public Text towerNameTextForPurchase, towerValueTextForPurchase;
    public BuyTowerButton[] buyTowerButtons;
    public TowerInteractions towerInteractions;

    [HideInInspector] public bool selectedTowerIsMoving;
    [HideInInspector] public Tower selectedTower;
    Ray ray;

    private void Awake() {
        tm_Single = this;
    }

    private void Start() {
        CheckPricesSetInteractableAndNot();
        SetTowerNameAndValueOnHover("", "");
    }

    public void Update() {
        ray = Movement.m_Single.topdownCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(1) && MenuManager.mm_Single.currentMenuState == MenuManager.MenuState.Closed) {
            UnSelectTower(false);
        }
        TowerSelectCheck();
        SelectedTowerPlacing();
    }

    public void PickTower(Tower pickedTower) {
        if (MenuManager.mm_Single.currentMenuState == MenuManager.MenuState.Closed) {
            Tower tower = Instantiate(pickedTower, Vector3.zero, Quaternion.identity);
            SelectTower(tower);
        }
    }

    public void SelectTower(Tower tower) {
        UnSelectTower(true);
        selectedTower = tower;
    }

    void TowerSelectCheck() {
        if (Input.GetMouseButtonDown(0) && !selectedTower && MenuManager.mm_Single.currentMenuState == MenuManager.MenuState.Closed) {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if (hit.transform.CompareTag(towerTag)) {
                    selectedTower = hit.transform.GetComponentInParent<Tower>();
                    towerInteractions.UpdateInterActionUi(selectedTower);
                }
            }
        }
    }

    void SelectedTowerPlacing() {
        if (selectedTower) {
            if (!selectedTower.placedOnParentTile && !selectedTower.canNeverMove) {
                RaycastHit hit;
                selectedTowerIsMoving = true;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileMask)) {
                    Vector3 newPos = hit.transform.position;
                    Vector3 newRot = selectedTower.transform.rotation.eulerAngles;
                    if (hit.transform.CompareTag(buildableTileTag)) {
                        Tile tile = hit.transform.GetComponent<Tile>();
                        if (tile) {
                            if (tile.buildable == true) {
                                selectedTower.ChangeTowerColor(canPlaceColor);
                                if (tile.buildableParent == null) {
                                    newPos = tile.transform.position;
                                    newRot = tile.setRotation;
                                } else {
                                    newPos = tile.buildableParent.transform.position;
                                    newRot = tile.buildableParent.setRotation;
                                }
                                if (Input.GetMouseButtonDown(0)) {
                                    BuyTower();
                                    tile.buildable = false;
                                    if (tile.buildableParent != null) {
                                        tile.buildableParent.buildable = false;
                                        tile = tile.buildableParent;
                                    } else {
                                        tile.buildableChild.buildable = false; 
                                    }
                                    selectedTower.PlaceOnParentTile(tile);
                                }
                            } else {
                                newPos = tile.setPosition;
                                newRot = selectedTower.transform.rotation.eulerAngles;
                                selectedTower.ChangeTowerColor(canNotPlaceColor);
                            }
                        }
                    } else {
                        selectedTower.ChangeTowerColor(canNotPlaceColor);
                    }
                    if (selectedTower) {
                        selectedTower.transform.eulerAngles = newRot;
                        selectedTower.transform.position = newPos;
                    }
                }
            }
        }
    }

    public void BuyTower() {
        if (GameManager.gm_Single.devMode == false && !selectedTower.oldParentTile) {
            ScrapManager.sm_single.AddOrWithdrawScrap(selectedTower.buyScrapPrice, ScrapManager.ScrapOption.Withdraw);
        }
    }

    public void SellTower(Tower tower) {
        ScrapManager.sm_single.AddOrWithdrawScrap(tower.sellScrapPrice, ScrapManager.ScrapOption.Add);
        tower.DetachFromParentTile();
        selectedTowerIsMoving = false;
        MenuManager.mm_Single.CloseMenu();
        selectedTower = null;
        Destroy(tower.gameObject);
    }

    public void MoveTower(Tower tower) {
        selectedTowerIsMoving = true;
        tower.DetachFromParentTile();
        MenuManager.mm_Single.CloseMenu();
    }

    public void UnSelectTower(bool destroyAlways) {
        if (selectedTower) {
            print("selected");
            if (!selectedTower.canNeverMove) {
            print("can move");
                if((selectedTower.oldParentTile || !selectedTowerIsMoving) && !destroyAlways) {
                    selectedTower.PlaceOnParentTile(selectedTower.oldParentTile);
            print("tile or not moving and not destroy always");
                } else {
                    Destroy(selectedTower.gameObject);
            print("destroy");
                }
            } else {
            print("can not move");
                selectedTower = null;
            }
        }
    }

    public void SetTowerNameAndValueOnHover(string towerName, string value) {
        towerNameTextForPurchase.text = towerName;
        towerValueTextForPurchase.text = value;
    }

    public void CheckPricesSetInteractableAndNot() {
        for (int i = 0; i < buyTowerButtons.Length; i++) {
            if (buyTowerButtons[i] && buyTowerButtons[i].tower) {
                if (buyTowerButtons[i].tower.buyScrapPrice <= ScrapManager.sm_single.currentScrap || GameManager.gm_Single.devMode) {
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

[System.Serializable]
public class TowerRotations {
    public Vector3 plusXRotation, minXRotation, plusZRotation, minZRotation;
}

[System.Serializable]
public class TowerInteractions {
    //ia == InterActions
    public Menu ia_Menu;
    public Image ia_TowerImage;
    public Text ia_TowerName, ia_TowerDescription, ia_ConfirmationText;
    public string towerIdentifier = "tower", priceIdentifier = "price", actionIdentifier = "action";
    [TextArea]
    public string ia_ConfirmationTextString = "Example action tower for price";
    public Button ia_MoveTower, ia_SellTower, ia_Cancel, ia_SellConfirm, ia_SellCancel;

    public void UpdateInterActionUi(Tower selectedTower) {
        ia_TowerImage.sprite = selectedTower.towerSprite;
        ia_TowerName.text = selectedTower.towerName;

        ia_TowerDescription.text = selectedTower.description;
        string newSellConfirm = ia_ConfirmationTextString.Replace(towerIdentifier, selectedTower.towerName);
        newSellConfirm = newSellConfirm.Replace(priceIdentifier, selectedTower.sellScrapPrice.ToString());
        ia_ConfirmationText.text = newSellConfirm;

        if (!selectedTower.canNeverMove) {
            ia_MoveTower.gameObject.SetActive(true);
            ia_SellTower.gameObject.SetActive(true);
        } else {
            ia_MoveTower.gameObject.SetActive(false);
            ia_SellTower.gameObject.SetActive(false);
        }

        ia_Cancel.onClick.RemoveAllListeners();
        ia_SellConfirm.onClick.RemoveAllListeners();
        ia_MoveTower.onClick.RemoveAllListeners();

        ia_Cancel.onClick.AddListener(() => TowerManager.tm_Single.UnSelectTower(false));
        ia_MoveTower.onClick.AddListener(() => TowerManager.tm_Single.MoveTower(selectedTower));
        ia_SellConfirm.onClick.AddListener(() => TowerManager.tm_Single.SellTower(selectedTower));
        ia_SellCancel.onClick.AddListener(() => TowerManager.tm_Single.UnSelectTower(false));

        MenuManager.mm_Single.OpenMenu(ia_Menu);
    }
}