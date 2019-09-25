using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public bool b;
    public Transform target;

    void Update() {
        Debug.DrawRay(transform.position, transform.forward, Color.red * 1000);
    }
}
