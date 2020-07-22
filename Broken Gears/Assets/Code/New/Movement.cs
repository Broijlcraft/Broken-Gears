using UnityEngine;

public class Movement : MonoBehaviour {
    public static Movement m_Single;
    public Transform pov;
    public float speed, mouseSensitivity;
    [Range(0, 90)]
    public float maxVerticalTopViewAngle = 30, maxVerticalBottomViewAngle = 30;
    Vector3 v;
    float xRotationAxisAngle;
    public bool canMove;

    private void Awake() {
        m_Single = this;
        xRotationAxisAngle = pov.rotation.eulerAngles.x * -1;
    }

    private void FixedUpdate() {
        if (canMove == true) {
            v.x = Input.GetAxis("Horizontal");
            v.z = Input.GetAxis("Vertical");
            if (Input.GetButton("Slow")) {
                transform.Translate(v * speed / 2);
            } else {
                transform.Translate(v * speed);
            }
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            RotateCam();
            if (Input.GetMouseButtonDown(2)) {

            }
        }
    }

    void RotateCam() {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotationAxisAngle += mouseY;

        if (xRotationAxisAngle > maxVerticalBottomViewAngle) {
            xRotationAxisAngle = maxVerticalBottomViewAngle;
            mouseY = 0f;
            ClampXRotationAxisToValue(pov, -maxVerticalBottomViewAngle);
        } else if (xRotationAxisAngle < -maxVerticalTopViewAngle) {
            xRotationAxisAngle = -maxVerticalTopViewAngle;
            mouseY = 0f;
            ClampXRotationAxisToValue(pov, maxVerticalTopViewAngle);
        }

        pov.Rotate(Vector3.left * mouseY);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void ClampXRotationAxisToValue(Transform transform_, float value) {
        Vector3 eulerRotation = transform_.localEulerAngles;
        eulerRotation.x = value;
        transform_.localEulerAngles = eulerRotation;
    }
}