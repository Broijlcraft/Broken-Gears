using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNoises : MonoBehaviour {

    public AudioSource attackSound;

    void Start() {
        Destroy(gameObject, attackSound.clip.length);
    }
}
