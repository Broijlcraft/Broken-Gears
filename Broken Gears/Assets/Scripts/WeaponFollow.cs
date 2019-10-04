using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFollow : MonoBehaviour {
    public GameObject weap;
    Turret Turret;
    Transform t;
    Vector3 v;
    public bool x;

    private void Start() {
        Turret = GetComponentInParent<Turret>();   
    }

    void Update() {
        if (Turret.armTarget != null) {
            Debug.DrawRay(transform.position, transform.forward, Color.yellow * 1000);
            Vector3 armDir = Turret.armTarget.position - Turret.transform.position;
            Quaternion armLookRotation = Quaternion.LookRotation(armDir);
            Vector3 armRotation = Quaternion.Lerp(transform.rotation, armLookRotation, Time.deltaTime * Turret.turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, armRotation.y, 0f);
        }

        if (weap != null && Turret.weaponTarget != null) {
            Debug.DrawRay(weap.transform.position, weap.transform.forward, Color.cyan * 1000);
            Vector3 weaponDir = Turret.weaponTarget.position - weap.transform.position;
            Quaternion weaponLookRotation = Quaternion.LookRotation(weaponDir);
            Vector3 weaponRotation = Quaternion.Lerp(weap.transform.rotation, weaponLookRotation, Time.deltaTime * Turret.turnSpeed).eulerAngles;
            if (x == false) {
                weap.transform.localRotation = Quaternion.Euler(0f, 0f, weaponRotation.z);
            } else {
                weap.transform.localRotation = Quaternion.Euler(weaponRotation.x, 0f, 0f);
            }
        }
    }
}
