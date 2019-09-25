using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeRotation : MonoBehaviour {
    public Vector3 rot;
    public float divider;
    public float speed;
    float actualSpeed;
    Weapon weapon;

    private void Start() {
        weapon = GetComponentInParent<Weapon>();
    }

    private void Update() {
        if (weapon.armTarget != weapon.defaultArmTarget) {
            if (actualSpeed < speed) {
                actualSpeed += speed / divider;
            }
        } else {
            if (actualSpeed > 0) {
                actualSpeed -= speed / divider;
            } else {
                actualSpeed = 0;
            }
        }
        transform.Rotate(rot * actualSpeed);
    }
}
