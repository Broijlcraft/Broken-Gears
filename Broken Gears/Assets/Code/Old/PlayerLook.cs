using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class PlayerLook : MonoBehaviour {
    public float mouseSensitivity, topLock, bottomLock, multiplier, xAxisClamp;

    public LayerMask layerMask;

    public Movement movement;
    public Color defaultTextColor, goodButtonColor, badButtonTextColor, badButtonColor;

    UiManager uiManager;
    Text buySellText;
    Button buySellButton;

    [Header("Animating")]

    public float speed;
    public bool moving;
    public int updates;
    public float amount;
    public static bool canMove;
    public float initialMoveDelay;
    int i;
    Vector3 v;

    delegate void TowerFunctionOverload();

    private void Start() {
        if (UiManager.staticMenuScript && UiManager.staticMenuScript.buySellText) {
            buySellText = UiManager.staticMenuScript.buySellText.GetComponentInChildren<Text>();
            buySellButton = UiManager.staticMenuScript.buySellText.GetComponent<Button>();
            uiManager = GameObject.Find("Canvas").GetComponentInChildren<UiManager>();
        }
    }

    private void Update() {
        RaycastHit hit;
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red * 1000);
        if (!GameManager.gm_Single.rework && Input.GetMouseButtonDown(1) && UiManager.staticMenuScript.menuState == MenuScript.MenuState.none && canMove == true) {
            Ray ray = Manager.cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if (hit.transform.gameObject.tag == "Turret" || hit.transform.gameObject.tag == "Scrap") {
                    UiManager.staticMenuScript.MenuSwitch("towerInteraction");
                    if (hit.transform.tag == "Scrap") {
                        if (hit.transform.GetComponent<SalvageTower>().bought == true) {
                            SetBuySellButton("Unlocked", false, Dummy, defaultTextColor, goodButtonColor);
                        } else {
                            SetBuySellButton("Unlock for " + hit.transform.GetComponent<SalvageTower>().price, true, () => hit.transform.GetComponent<SalvageTower>().ActivateTower(), defaultTextColor, Color.white);
                        }
                    }
                    if (hit.transform.tag == "Turret") {
                        if (TowerManager.activeScrapTower > 0) {
                            SetBuySellButton("Salvage for " + hit.transform.GetComponentInParent<SelectTowerPlacement>().salvageValue, true, () => hit.transform.GetComponentInParent<SelectTowerPlacement>().SellTower(), defaultTextColor, Color.white);
                        } else {
                            SetBuySellButton("Salvage Furnace Required", false, Dummy, badButtonTextColor, badButtonColor);
                        }
                    }
                }
            }
        }
    }

    void Dummy() {

    }

    public void SellTower() {
        print("sell");
    }
    
    public void StartGame() {

    }

    void SetBuySellButton(string s, bool b, TowerFunctionOverload tfo, Color textColor, Color disabledButtonColor) {
        buySellText.text = s;
        buySellText.color = textColor;
        ColorBlock block = buySellButton.colors;
        block.disabledColor = disabledButtonColor;
        buySellButton.colors = block;
        buySellButton.interactable = b;
        buySellButton.onClick.RemoveAllListeners();
        buySellButton.onClick.AddListener(() => tfo());
    }

    private void LateUpdate() {
        if (MenuManager.mm_Single.currentMenuState == MenuManager.MenuState.Closed) {
            if (Input.GetMouseButton(2) && moving == false && canMove == true) {
                VerticalCameraRotation();
                //movement.HorizontalCameraRotation();
            } else if (moving == true) {
                if (i < updates) {
                    v = transform.localEulerAngles;
                    v.x -= amount;
                    i++;
                    transform.localRotation = Quaternion.Euler(v);
                } else {
                    moving = false;
                    canMove = true;
                    uiManager.TurnOnOff(true);
                }
            }
        }
    }

    private void VerticalCameraRotation() {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > -topLock) {
            xAxisClamp = -topLock;
            mouseY = 0f;
            ClampXAxisRotationToValue(topLock);
        } else if (xAxisClamp < -bottomLock) {
            xAxisClamp = -bottomLock;
            mouseY = 0f;
            ClampXAxisRotationToValue(bottomLock);
        }
        transform.Rotate(Vector3.left * mouseY);
    }

    public void UpdateLookValue() {
        mouseSensitivity = movement.mouseSensitivity * multiplier;
    }

    private void ClampXAxisRotationToValue(float value) {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}