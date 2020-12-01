using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TowerManager : MonoBehaviour {
    public static TowerManager singleTM;

    [SerializeField] private LayerMask layersToIgnoreWhenAttacking, tileMask;
    [SerializeField] private string buildableTileTag = "BuildableTile", towerTag = "Tower";
    [SerializeField] private Color canPlaceColor, canNotPlaceColor;

    [SerializeField] private TowerRotations towerRotations = new TowerRotations();
    [SerializeField] private Text towerNameTextForPurchase, towerValueTextForPurchase;
    [SerializeField] private BuyTowerButton[] buyTowerButtons = new BuyTowerButton[0];
    [SerializeField] private TowerInteractions towerInteractions = new TowerInteractions();

    private bool selectedTowerIsMoving;
    private Tower selectedTower;
    private Ray ray;
    private List<Tower> towersOnTheField = new List<Tower>();

    #region Get/Set
    public Vector3 GetTowerRotation(Transform parent, Transform child) {
        Vector3 towerRotation = Vector3.zero;
        if (parent.position.x == child.position.x) {
            if (parent.position.z > transform.position.z) {
                towerRotation = towerRotations.minZRotation;
            } else {
                towerRotation = towerRotations.plusZRotation;
            }
        } else if (parent.position.z == transform.position.z) {
            if (parent.position.x > child.position.x) {
                towerRotation = towerRotations.minXRotation;
            } else {
                towerRotation = towerRotations.plusXRotation;
            }
        }
        return towerRotation;
    }

    public TowerRotations GetTowerRotations() {
        return towerRotations;
    }

    public LayerMask GetIgnoreLayers() {
        return layersToIgnoreWhenAttacking;
    }

    public Tower GetSelectedTower() {
        return selectedTower;
    }

    public void SetSelectedTower(Tower tower) {
        selectedTower = tower;
    }

    public bool GetSelectedTowerIsMoving() {
        return selectedTowerIsMoving;
    }

    public void SetSelectedTowerIsMoving(bool state) {
        selectedTowerIsMoving = state;
    }
    #endregion

    private void Awake() {
        singleTM = this;
    }

    private void Start() {
        CheckPricesSetInteractableAndNot();
        SetTowerNameAndValueOnHover("", "");
    }

    public void Update() {
        ray = Movement.m_Single.topdownCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(1) && MenuManager.mm_Single.currentMenuState == MenuState.Closed) {
            UnSelectTower(false);
        }
        TowerSelectCheck();
        SelectedTowerPlacing();
    }

    public void Restart() {
        for (int i = 0; i < towersOnTheField.Count; i++) {
            Destroy(towersOnTheField[i].gameObject);
        }
    }

    public void PickTower(Tower pickedTower) {
        if (MenuManager.mm_Single.currentMenuState == MenuState.Closed) {
            Tower tower = Instantiate(pickedTower, Vector3.zero, Quaternion.identity);
            SelectTower(tower);
        }
    }

    public void SelectTower(Tower tower) {
        UnSelectTower(true);
        selectedTower = tower;
    }

    void TowerSelectCheck() {
        if (Input.GetMouseButtonDown(0) && !selectedTower && MenuManager.mm_Single.currentMenuState == MenuState.Closed) {
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
            if (!selectedTower.placedOnParentTile && !selectedTower.GetCanNeverMove()) {
                RaycastHit hit;
                selectedTowerIsMoving = true;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileMask)) {
                    Vector3 newPos = hit.transform.position;
                    Vector3 newRot = selectedTower.transform.rotation.eulerAngles;
                    if (hit.transform.CompareTag(buildableTileTag)) {
                        Tile tile = hit.transform.GetComponent<Tile>();
                        if (tile) {
                            if (tile.GetIsBuildable() == true) {
                                selectedTower.ChangeTowerColor(canPlaceColor);

                                Tile bParent = tile.GetBuildableParent();
                                if (!bParent) {
                                    newPos = tile.transform.position;
                                    newRot = tile.GetTargetRotation();
                                } else {
                                    newPos = bParent.transform.position;
                                    newRot = bParent.GetTargetRotation();
                                }

                                if (Input.GetMouseButtonDown(0)) {
                                    towersOnTheField.Add(selectedTower);
                                    BuyTower();
                                    tile.SetBuildable(false);
                                    if (bParent) {
                                        bParent.SetBuildable(false);
                                        tile = bParent;
                                    } else {
                                        tile.GetBuildableChild().SetBuildable(false); 
                                    }
                                    selectedTower.PlaceOnParentTile(tile);
                                }
                            } else {
                                newPos = tile.GetTargetPosition();
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
        if (GameManager.gm_Single.DevMode() == false && !selectedTower.oldParentTile) {
            ScrapManager.sm_single.AddOrWithdrawScrap(selectedTower.GetBuyScrapPrice(), ScrapManager.ScrapOption.Withdraw);
        }
    }

    public void SellTower(Tower tower) {
        ScrapManager.sm_single.AddOrWithdrawScrap(tower.GetSellScrapPrice(), ScrapManager.ScrapOption.Add);
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
            if (!selectedTower.GetCanNeverMove()) {
                if((selectedTower.oldParentTile || !selectedTowerIsMoving) && !destroyAlways) {
                    selectedTower.PlaceOnParentTile(selectedTower.oldParentTile);
                } else {
                    Destroy(selectedTower.gameObject);
                }
            } else {
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
            BuyTowerButton towerButton = buyTowerButtons[i];

            if (towerButton) {
                Tower tower = towerButton.GetTower();
                Button button = towerButton.GetButton();

                if (tower) {
                    if (tower.GetBuyScrapPrice() <= ScrapManager.sm_single.GetCurrentScrap() || GameManager.gm_Single.DevMode()) {
                        button.interactable = true;
                    } else {
                        button.interactable = false;
                    }
                } else {
                    button.interactable = false;
                }
            }
        }
    }
}

[System.Serializable]
public struct TowerRotations {
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
        string towerName = selectedTower.GetTowerName();

        ia_TowerImage.sprite = selectedTower.GetTowerSprite();
        ia_TowerName.text = towerName;

        ia_TowerDescription.text = selectedTower.GetDescription();
        string newSellConfirm = ia_ConfirmationTextString.Replace(towerIdentifier, towerName);
        newSellConfirm = newSellConfirm.Replace(priceIdentifier, selectedTower.GetSellScrapPrice().ToString());
        ia_ConfirmationText.text = newSellConfirm;

        if (!selectedTower.GetCanNeverMove()) {
            ia_MoveTower.gameObject.SetActive(true);
            ia_SellTower.gameObject.SetActive(true);
        } else {
            ia_MoveTower.gameObject.SetActive(false);
            ia_SellTower.gameObject.SetActive(false);
        }

        ia_Cancel.onClick.RemoveAllListeners();
        ia_SellConfirm.onClick.RemoveAllListeners();
        ia_MoveTower.onClick.RemoveAllListeners();

        ia_Cancel.onClick.AddListener(() => TowerManager.singleTM.UnSelectTower(false));
        ia_MoveTower.onClick.AddListener(() => TowerManager.singleTM.MoveTower(selectedTower));
        ia_SellConfirm.onClick.AddListener(() => TowerManager.singleTM.SellTower(selectedTower));
        ia_SellCancel.onClick.AddListener(() => TowerManager.singleTM.UnSelectTower(false));

        MenuManager.mm_Single.OpenMenu(ia_Menu);
    }
}