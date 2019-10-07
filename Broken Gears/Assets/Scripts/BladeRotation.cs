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
            }
        } else {
            if (actualSpeed > 0) {
                actualSpeed -= speed / divider;
            } else {
                actualSpeed = 0;
            }
        }
        transform.Rotate(rot * actualSpeed * Time.deltaTime);
    }

    //private void OnTriggerEnter(Collider other) {
    //    if (other.transform.tag == "Enemy") {
    //        print("Collision");
    //    }
    //}

    //private void OnTriggerExit(Collider other) {
        
    //}
}
