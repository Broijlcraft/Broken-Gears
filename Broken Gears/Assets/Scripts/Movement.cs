using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    Vector3 v;
    public float speed;
    GameObject holder;
    GameObject cam;
    public float rotationSpeed;
    public Vector2 camClamp;
    Vector3 hor;

    private void Start() {
        holder = transform.GetChild(0).gameObject;
        cam = holder.transform.GetChild(0).gameObject;
    }

    private void FixedUpdate() {
        v.x = Input.GetAxis("Horizontal");
        v.z = Input.GetAxis("Vertical");
        transform.Translate(v * speed * Time.deltaTime);

        hor.y = Input.GetAxis("Mouse X") * rotationSpeed;
        float yRot = Input.GetAxis("Mouse Y") * (rotationSpeed * rotationSpeed) * Time.deltaTime;
        Vector3 rot = cam.transform.localRotation.eulerAngles + new Vector3(-yRot, 0f, 0f);
        rot.x = ClampAngle(rot.x, -camClamp.x, camClamp.y);

        cam.transform.localEulerAngles = rot;
        transform.Rotate(hor * (rotationSpeed * Time.deltaTime));
    }

    float ClampAngle(float angle, float from, float to) {
        if (angle < 0f) {
            angle = 360 + angle;
        }
        if (angle > 180f) {
            return Mathf.Max(angle, 360 + from);
        }
        return Mathf.Min(angle, to);
    }
}
