using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSlideFollow : MonoBehaviour {

    public bool beam;
    public bool slide;
    GameObject camControl;
    Vector3 v;

    private void Start() {
        camControl = GameObject.Find("CamControl");
    }

    private void Update() {
        if (beam == true) {
            v = transform.position;
            v.z = camControl.transform.position.z;
            transform.position = v;
        } else if (slide == true){
            v = transform.position;
            v.z = camControl.transform.position.z;
            v.x = camControl.transform.position.x;
            transform.position = v;
        }
    }
}
