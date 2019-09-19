using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFollow : MonoBehaviour {
    public GameObject gunBarrel;
    public GameObject weap;
    Weapon weapon;
    Transform t;
    Vector3 v;
    public float turnSpeed;

    private void Start() {
        weapon = GetComponentInParent<Weapon>();   
    }
    void Update() {
        Vector3 dir = weapon.target.position - weap.transform.position;
        Quaternion armLookRotation = Quaternion.LookRotation(dir);
        Quaternion gunLookRotation = Quaternion.LookRotation(dir);
        Vector3 armRotation = Quaternion.Lerp(transform.rotation, armLookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        Vector3 gunRotation = Quaternion.Lerp(weap.transform.rotation, gunLookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, armRotation.y, 0f);
        weap.transform.localRotation = Quaternion.Euler(0f, 0f, gunRotation.z);
        Debug.DrawRay(gunBarrel.transform.position, gunBarrel.transform.forward, Color.blue * 1000);
    }
}
