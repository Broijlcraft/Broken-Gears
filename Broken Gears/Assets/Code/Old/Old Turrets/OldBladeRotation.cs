using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldBladeRotation : MonoBehaviour {
    public Vector3 rot;
    public float divider, speed;
    float actualSpeed;
    OldTurret turret;
    bool isColliding;

    private void Start() {
        turret = GetComponentInParent<OldTurret>();
        InvokeRepeating("SepCollCheck", 0f, 0.07f);
    }

    public void SepCollCheck() {
        if (turret.armTarget == turret.defaultArmTarget) {
            isColliding = false;
            StopColliding();
        }
    }

    private void Update() {
        if (OldTowerManager.old_tm_Single.selectedTower != turret.gameObject) {
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
        if (other.transform.tag == "Enemy" && OldTowerManager.old_tm_Single.selectedTower != turret.gameObject) {
            turret.sawCollision = true;
            isColliding = true;
            turret.transform.GetComponentInChildren<OldParticleScript>().Particular();
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
            turret.transform.GetComponentInChildren<OldParticleScript>().Particular();
        }
    }
}
