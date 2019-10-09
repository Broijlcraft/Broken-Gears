using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFollow : MonoBehaviour {
    public Transform weap;
    public float delay;
    Turret Turret;
    Transform t;
    Vector3 v;
    public bool x;
    Vector3 oldPos;
    Vector3 diff;
    Vector3 newPos;

    private void Start() {
        Turret = GetComponentInParent<Turret>();
        InvokeRepeating("UpdatePosition", 0f, delay);
    }

    void Update() {
        if (Turret.selected == false) {
            if (Turret.armTarget != null) {
                Debug.DrawRay(transform.position, transform.forward, Color.yellow * 1000);
                Vector3 armDir = newPos - Turret.transform.position;
                Quaternion armLookRotation = Quaternion.LookRotation(armDir);
                Vector3 armRotation = Quaternion.Lerp(transform.rotation, armLookRotation, Time.deltaTime * Turret.turnSpeed).eulerAngles;
                transform.rotation = Quaternion.Euler(0f, armRotation.y, 0f);
            }

            if (weap != null && Turret.weaponTarget != null) {
                Debug.DrawRay(weap.position, weap.forward, Color.cyan * 1000);
                Vector3 weaponDir = Turret.weaponTarget.position - weap.position;
                Quaternion weaponLookRotation = Quaternion.LookRotation(weaponDir);
                Vector3 weaponRotation = Quaternion.Lerp(weap.rotation, weaponLookRotation, Time.deltaTime * Turret.turnSpeed).eulerAngles;
                if (x == false) {
                    weap.localRotation = Quaternion.Euler(0f, 0f, weaponRotation.z);
                } else {
                    weap.localRotation = Quaternion.Euler(weaponRotation.x, 0f, 0f);
                }
            }
            //if (Turret.armTarget != null) {
            //    Debug.DrawRay(transform.position, transform.forward, Color.yellow * 1000);
            //    Vector3 armDir = Turret.armTarget.position - Turret.transform.position;
            //    Quaternion armLookRotation = Quaternion.LookRotation(armDir);
            //    Vector3 armRotation = Quaternion.Lerp(transform.rotation, armLookRotation, Time.deltaTime * Turret.turnSpeed).eulerAngles;
            //    transform.rotation = Quaternion.Euler(0f, armRotation.y, 0f);
            //}

            //if (weap != null && Turret.weaponTarget != null) {
            //    Debug.DrawRay(weap.position, weap.forward, Color.cyan * 1000);
            //    Vector3 weaponDir = Turret.weaponTarget.position - weap.position;
            //    Quaternion weaponLookRotation = Quaternion.LookRotation(weaponDir);
            //    Vector3 weaponRotation = Quaternion.Lerp(weap.rotation, weaponLookRotation, Time.deltaTime * Turret.turnSpeed).eulerAngles;
            //    if (x == false) {
            //        weap.localRotation = Quaternion.Euler(0f, 0f, weaponRotation.z);
            //    } else {
            //        weap.localRotation = Quaternion.Euler(weaponRotation.x, 0f, 0f);
            //    }
            //}
        }
    }

    void UpdatePosition() {
        if (Turret.armTarget != null) {

            oldPos = Turret.armTarget.position;
        }
    }
}
