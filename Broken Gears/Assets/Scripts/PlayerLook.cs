using System.Collections;
using System.Collections.Generic;
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

    MenuScript menuScript;

    Text buySellText;
    Button buySellButton;

    private void Awake() {
        UpdateLookValue();
        xAxisClamp = 0f;
        VerticalCameraRotation();
        menuScript = GameObject.Find("Canvas").GetComponentInChildren<MenuScript>();
        buySellText = menuScript.buySellText.GetComponentInChildren<Text>();
        buySellButton = menuScript.buySellText.GetComponent<Button>();
    }

    private void Update() {
        RaycastHit hit;
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red * 1000);
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Manager.cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                menuScript.MenuSwitch("towerInteraction");
                if (hit.transform.tag == "Scrap") {
                    if(hit.transform.GetComponent<SalvageTower>().bought == true) {
                        buySellText.text = "Bought";
                        buySellButton.interactable = false;
                    } else {
                        buySellText.text = "Buy for " + hit.transform.GetComponent<SalvageTower>().price;
                        buySellButton.interactable = true;
                    }
                } else if (hit.transform.tag == "Turret") {
                    if (TowerManager.activeScrapTower > 0) {

                    } else {

                    }
                }
            }
        }
    }

    private void LateUpdate() {
        if (Input.GetMouseButton(2)) {
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