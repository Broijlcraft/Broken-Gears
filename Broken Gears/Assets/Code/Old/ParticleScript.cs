using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {

    Turret turret;
    ParticleSystem particle;
    public float delay;

    private void Awake() {
        turret = GetComponentInParent<Turret>();
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
