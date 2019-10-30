using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class PlayerLook : MonoBehaviour {
    public float mouseSensitivity;

    public float topLock;
    public float bottomLock;
    public float multiplier;
    private float xAxisClamp;

    public LayerMask layerMask;

    public Movement movement;
    public Color defaultTextColor;
    public Color goodButtonColor;
    public Color badButtonColor;

    Text buySellText;
    Button buySellButton;

    delegate void TowerFunctionOverload();

    private void Awake() {
        UpdateLookValue();
        xAxisClamp = 0f;
        VerticalCameraRotation();
        buySellText = UiManager.staticMenuScript.buySellText.GetComponentInChildren<Text>();
        buySellButton = UiManager.staticMenuScript.buySellText.GetComponent<Button>();
    }

    private void Update() {
        RaycastHit hit;
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red * 1000);
        if (Input.GetMouseButtonDown(1) && UiManager.staticMenuScript.menuState == MenuScript.MenuState.none) {
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
                            SetBuySellButton("Salvage Furnace Required", false, Dummy, defaultTextColor, badButtonColor);
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
        if (Input.GetMouseButton(2) && UiManager.staticMenuScript.menuState == MenuScript.MenuState.none) {
            VerticalCameraRotation();
            movement.HorizontalCameraRotation();
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
        mouseSensitivity = movement.rotationSpeed * multiplier;
    }

    private void ClampXAxisRotationToValue(float value) {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}