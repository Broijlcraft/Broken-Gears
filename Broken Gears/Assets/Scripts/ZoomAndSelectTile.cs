using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndSelectTile : MonoBehaviour {

    public float zoom;
    public float zoomIncrease;
    public float maxZoomIn;
    public float maxZoomOut;
    Camera cam;

    private void Start() {
        cam = GetComponent<Camera>();
        if (zoom == 0) {
            zoom = cam.fieldOfView;
        } else {
            cam.fieldOfView = zoom;
        }
    }

    private void Update() {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            if (zoom > maxZoomIn) {
                zoom -= zoomIncrease;
                cam.fieldOfView = zoom;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            if (zoom < maxZoomOut) {
                zoom += zoomIncrease;
                cam.fieldOfView = zoom;
            }
        }
    }
}
