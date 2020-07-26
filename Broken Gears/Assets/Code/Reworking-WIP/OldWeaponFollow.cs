using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldWeaponFollow : MonoBehaviour {
    public Transform weap;
    OldTurret turret;
    public bool x;

    private void Start() {
        turret = GetComponentInParent<OldTurret>();   
    }

    void Update() {
        if (turret.selected == false) {
            if (turret.armTarget != null) {
                Debug.DrawRay(transform.position, transform.forward, Color.yellow * 1000);
                Vector3 armDir = turret.armTarget.position - turret.transform.position;
                Quaternion armLookRotation = Quaternion.LookRotation(armDir);
                Vector3 armRotation = Quaternion.Lerp(transform.rotation, armLookRotation, Time.deltaTime * turret.turnSpeed).eulerAngles;
                transform.rotation = Quaternion.Euler(0f, armRotation.y, 0f);
            }

            if (weap != null && turret.weaponTarget != null) {
                Debug.DrawRay(weap.position, weap.forward, Color.cyan * 1000);
                Vector3 weaponDir = turret.weaponTarget.position - weap.position;
                Quaternion weaponLookRotation = Quaternion.LookRotation(weaponDir);
                Vector3 weaponRotation = Quaternion.Lerp(weap.rotation, weaponLookRotation, Time.deltaTime * turret.turnSpeed).eulerAngles;
                if (x == false) {
                    weap.localRotation = Quaternion.Euler(0f, 0f, weaponRotation.z);
                } else {
                    weap.localRotation = Quaternion.Euler(weaponRotation.x, 0f, 0f);
                }
            }
        }
    }
}
