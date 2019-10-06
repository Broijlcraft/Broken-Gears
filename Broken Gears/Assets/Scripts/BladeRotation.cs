using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeRotation : MonoBehaviour {
    public Vector3 rot;
    public float divider;
    public float speed;
    float actualSpeed;
    Turret turret;

    private void Start() {
        turret = GetComponentInParent<Turret>();
    }

    private void Update() {
        if (turret.armTarget != turret.defaultArmTarget) {
            if (actualSpeed < speed) {
                actualSpeed += speed / divider;
            } else {
                print("MaxSpeed");
            }
        } else {
            if (actualSpeed > 0) {
                actualSpeed -= speed / divider;
            } else {
                actualSpeed = 0;
                print(0);
            }
        }
        transform.Rotate(rot * actualSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Enemy") {
            print("Collision");
        }
    }
}
