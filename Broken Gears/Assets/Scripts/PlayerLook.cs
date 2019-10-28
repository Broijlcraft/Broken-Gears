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

    public Movement movement;

    [Header("Scraptower")]

    public List<GameObject> activeEffects = new List<GameObject>();
    public Vector4 disabledColor;
    public GameObject turret;

    MenuScript menuScript;

    private void Awake() {
        UpdateLookValue();
        xAxisClamp = 0f;
        VerticalCameraRotation();
        menuScript = GameObject.Find("Canvas").GetComponentInChildren<MenuScript>();
    }

    private void Update() {
        RaycastHit hit;
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red * 1000);
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Manager.cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if (hit.transform.tag == "Scrap") {
                    if(hit.transform.GetComponent<SalvageTower>().bought == true) {
                        menuScript.buySellText.GetComponentInChildren<Text>().text = "Bought";
                        menuScript.buySellText.GetComponent<Button>().interactable = false;
                    } else {
                        menuScript.buySellText.GetComponentInChildren<Text>().text = "Buy for " + hit.transform.GetComponent<SalvageTower>().price;
                        menuScript.buySellText.GetComponent<Button>().interactable = true;
                    }
                    //menuScript.MenuSwitch("towerInteraction");
                } else if (hit.transform.tag == "Turret") {
                    print("Turret");
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