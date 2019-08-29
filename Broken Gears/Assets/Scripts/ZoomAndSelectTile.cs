using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndSelectTile : MonoBehaviour {

    public float zoom = 50;
    Camera cam;

    private void Start() {
        cam = GetComponent<Camera>();
        cam.fieldOfView = zoom;
    }
}
