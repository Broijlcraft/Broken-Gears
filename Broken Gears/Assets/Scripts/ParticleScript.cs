using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {
    Turret turret;
    Transform target;
    private void Start() {
        turret = GetComponentInParent<Turret>();
        target = turret.armTarget;
    }

    private void Update() {
        if (turret.sawCollision == false) {
            transform.GetComponentInChildren<ParticleSystem>().Stop();
        } else {
            transform.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}
