using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DevMovement : MonoBehaviour {

    float scroll;
    public float speed = 5;
    Vector3 v;
    Vector3 vB;
    Vector3 hor;
    Vector3 ver;
    public float rotationSpeed;
    public Vector2 camClamp;
    public GameObject cam;

    private void FixedUpdate() {
        //v.x = Input.GetAxis("Horizontal");
        ////v.x = cam.transform.forward.x;
        //v.z = Input.GetAxis("Vertical");
        if (Input.GetAxis("Vertical") > 0) {
            transform.Translate(cam.transform.forward * speed * Time.deltaTime);
        } else {
            if (Input.GetAxis("Vertical") < 0) {
                transform.Translate(cam.transform.forward * -speed * Time.deltaTime);
            }
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

        hor.y = Input.GetAxis("Mouse X") * rotationSpeed;
        float yRot = Input.GetAxis("Mouse Y") * (rotationSpeed * rotationSpeed) * Time.deltaTime;
        Vector3 rot = cam.transform.localRotation.eulerAngles + new Vector3(-yRot, 0f, 0f);
        rot.x = ClampAngle(rot.x, -camClamp.x, camClamp.y);

        cam.transform.localEulerAngles = rot;
        transform.Rotate(hor * (rotationSpeed * Time.deltaTime));
    }
}
