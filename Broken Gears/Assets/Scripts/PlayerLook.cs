using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {
    public float mouseSensitivity;

    public float topLock;
    public float bottomLock;

    private float xAxisClamp;

    private void Awake() {
        xAxisClamp = 0.0f;
        CameraRotation();
    }

    private void Update() {
        //if (Input.GetMouseButton(2)) {
        //    CameraRotation();
        //}
        CameraRotation();
    }

    private void CameraRotation() {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > -topLock) {
            xAxisClamp = -topLock;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(topLock);
        } else if (xAxisClamp < -bottomLock) {
            xAxisClamp = -bottomLock;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(bottomLock);
        }

        transform.Rotate(Vector3.left * mouseY);
    }

    private void ClampXAxisRotationToValue(float value) {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}