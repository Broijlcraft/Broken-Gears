using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFollow : MonoBehaviour {
    public GameObject weap;
    Weapon weapon;
    Transform t;
    Vector3 v;

    private void Start() {
        weapon = GetComponentInParent<Weapon>();   
    }

    void Update() {
        if (weapon.target != null) {
            Vector3 dir = weapon.target.position - weap.transform.position;
            Quaternion armLookRotation = Quaternion.LookRotation(dir);
            Quaternion weaponLookRotation = Quaternion.LookRotation(dir);
            Vector3 armRotation = Quaternion.Lerp(transform.rotation, armLookRotation, Time.deltaTime * weapon.turnSpeed).eulerAngles;
            Vector3 weaponRotation = Quaternion.Lerp(weap.transform.rotation, weaponLookRotation, Time.deltaTime * weapon.turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, armRotation.y, 0f);
            weap.transform.localRotation = Quaternion.Euler(0f, 0f, weaponRotation.z);
        }
    }
}
