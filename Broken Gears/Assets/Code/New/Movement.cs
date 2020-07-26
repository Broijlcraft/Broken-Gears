using UnityEngine;

public class Movement : MonoBehaviour {
    public static Movement m_Single;
    public bool mouseClickUnlock;
    [Space]
    public Transform topdownCameraHolder;
    public Camera topdownCamera;
    public float speed, mouseSensitivity, maxZoomIn, maxZoomOut;
    public Range cameraBlockMoverange, beamMoverange;
    [Range(0, 90)]
    public float maxVerticalTopViewAngle = 30, maxVerticalBottomViewAngle = 30;
    float xRotationAxisAngle;
    [Header("WIP"), Space]
    public bool canMove;
    public float currentZoom = 50, zoomSensitivity;

    private void Awake() {
        m_Single = this;
        xRotationAxisAngle = topdownCamera.transform.rotation.eulerAngles.x * -1;
    }

    private void Update() {
        if (MenuManager.mm_Single.currentMenuState == MenuManager.MenuState.Closed && canMove == true) {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) {
                if (currentZoom > maxZoomIn) {
                    currentZoom -= zoomSensitivity;
                    if (currentZoom < maxZoomIn) {
                        currentZoom = maxZoomIn;
                    }
                }
            } else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
                if (currentZoom < maxZoomOut) {
                    currentZoom += zoomSensitivity;
                    if (currentZoom > maxZoomOut) {
                        currentZoom = maxZoomOut;
                    }
                }
            }
            topdownCamera.fieldOfView = currentZoom;
        }
    }

    private void FixedUpdate() {
        if (canMove == true) {
            Vector3 translatePos = Vector3.zero;
            translatePos.z = Input.GetAxis("Vertical");
            translatePos.x = Input.GetAxis("Horizontal"); 
            if (Input.GetButton("Slow")) {
                transform.Translate(translatePos * speed / 2);
            } else {
                transform.Translate(translatePos * speed);
            }
            if (mouseClickUnlock) { if (!Input.GetMouseButton(2)) { return; } }
            RotateCam();
        }
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, cameraBlockMoverange.min, cameraBlockMoverange.max);
        pos.z = Mathf.Clamp(pos.z, beamMoverange.min, beamMoverange.max);
        transform.position = pos;
    }

    void RotateCam() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotationAxisAngle += mouseY;

        if (xRotationAxisAngle > maxVerticalBottomViewAngle) {
            xRotationAxisAngle = maxVerticalBottomViewAngle;
            mouseY = 0f;
            ClampXRotationAxisToValue(topdownCamera.transform, -maxVerticalBottomViewAngle);
        } else if (xRotationAxisAngle < -maxVerticalTopViewAngle) {
            xRotationAxisAngle = -maxVerticalTopViewAngle;
            mouseY = 0f;
            ClampXRotationAxisToValue(topdownCamera.transform, maxVerticalTopViewAngle);
        }

        topdownCamera.transform.Rotate(Vector3.left * mouseY);
        topdownCameraHolder.Rotate(Vector3.up * mouseX);
    }

    private void ClampXRotationAxisToValue(Transform transform_, float value) {
        Vector3 eulerRotation = transform_.localEulerAngles;
        eulerRotation.x = value;
        transform_.localEulerAngles = eulerRotation;
    }

    public void OnMouseSensitivityChanged(float value) {
        mouseSensitivity = value;
    }

    public void OnZoomSensitivityChanged(float value) {
        zoomSensitivity = value;
    }
}