using System.Collections;
using System.Collections.Generic;
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

    private void Awake() {
        UpdateLookValue();
        xAxisClamp = 0f;
        VerticalCameraRotation();
    }

    private void Update() {
        RaycastHit hit;
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red * 1000);
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity)) {
                if (hit.transform.tag == "Scrap") {

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