using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomper : MonoBehaviour {
    public Vector3 down;

    void Stomp() {
        Vector3 armDir = transform.position - down;
        Quaternion armLookRotation = Quaternion.LookRotation(armDir);
        //Vector3 armRotation = Quaternion.Lerp(transform.rotation, armLookRotation, Time.deltaTime * weapon.turnSpeed).eulerAngles;
        //transform.rotation = Quaternion.Euler(0f, armRotation.y, 0f);
    }
}
