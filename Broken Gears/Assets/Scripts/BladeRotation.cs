using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeRotation : MonoBehaviour {
    public Vector3 rot;
    public float divider;
    public float speed;
    float actualSpeed;
    Turret turret;
    bool isColliding;

    private void Start() {
        turret = GetComponentInParent<Turret>();
        InvokeRepeating("SepCollCheck", 0f, 0.07f);
    }

    public void SepCollCheck() {
        if (turret.armTarget == turret.defaultArmTarget) {
            isColliding = false;
            StopColliding();
            //InvokeRepeating("StopColliding", 0.3f, 0f);
            print("stop");
        }
    }

    private void Update() {
        if (TowerManager.selectedTower != turret.gameObject) {
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
        }
        transform.Rotate(rot * actualSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Enemy" && TowerManager.selectedTower != turret.gameObject) {
            turret.sawCollision = true;
            isColliding = true;
            turret.transform.GetComponentInChildren<ParticleScript>().Particular();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.tag == "Enemy") {
            InvokeRepeating("StopColliding", 0.2f, 0f);
            isColliding = false;
        }
    }

    void StopColliding() {
        if (isColliding == false) {
            turret.sawCollision = false;
            turret.transform.GetComponentInChildren<ParticleScript>().Particular();
        }
    }
}
