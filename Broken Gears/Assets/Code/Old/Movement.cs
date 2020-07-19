using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    Vector3 v;
    public float speed, rotationSpeed;
    Vector3 hor;

    private void FixedUpdate() {
        if (PlayerLook.canMove == true) {
            v.x = Input.GetAxis("Horizontal");
            v.z = Input.GetAxis("Vertical");
            if (Input.GetButton("Slow")) {
                transform.Translate(v * speed / 2);
            } else {
                transform.Translate(v * speed);
            }
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void HorizontalCameraRotation() {
        hor.y = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(hor * (rotationSpeed * Time.deltaTime));
    }
}
