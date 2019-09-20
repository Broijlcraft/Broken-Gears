using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour {
    private void Update() {
        Debug.DrawRay(transform.position, transform.forward, Color.cyan * 1000);
    }
}
