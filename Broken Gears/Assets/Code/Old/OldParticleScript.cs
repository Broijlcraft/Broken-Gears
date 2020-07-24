using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldParticleScript : MonoBehaviour {

    OldTurret turret;
    ParticleSystem particle;
    public float delay;

    private void Awake() {
        turret = GetComponentInParent<OldTurret>();
        particle = transform.GetComponentInChildren<ParticleSystem>();
        particle.Stop();
    }

    public void Particular() {
        if (turret.sawCollision == false) {
            particle.Stop();
            InvokeRepeating("U", delay, 0);
        } else {
            particle.Play();
        }
    }
    void U() {
        particle.Clear();
    }
}
