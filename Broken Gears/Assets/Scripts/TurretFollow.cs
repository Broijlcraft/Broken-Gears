using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFollow : MonoBehaviour {
    public Transform enemy;
    public GameObject gun;
    public GameObject gunBarrel;
    Transform t;
    Vector3 v;
    void Update() {
        transform.LookAt(enemy);
        gun.transform.LookAt(enemy);
        Debug.DrawRay(transform.position, transform.forward, Color.red * 1000);
        Debug.DrawRay(gunBarrel.transform.position, gunBarrel.transform.forward, Color.blue * 1000);
    }
}
