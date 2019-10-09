using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScript : MonoBehaviour {

    public float zoom;
    public float zoomIncrease;
    public float maxZoomIn;
    public float maxZoomOut;
    public LayerMask layerMask;
    public RaycastHit hit;
    public GameObject g;
    
    Camera monitorCam;

    private void Start() {
        Manager.cam = GetComponent<Camera>();
        if (Manager.staticMonitor == true) {
            monitorCam = transform.GetChild(0).GetComponent<Camera>();
        }
        if (zoom == 0) {
            zoom = Manager.cam.fieldOfView;
        } else {
            Manager.cam.fieldOfView = zoom;
            if (Manager.staticMonitor == true) {
                monitorCam.fieldOfView = zoom;
            }
        }
    }

    private void Update() {
        if (Time.timeScale != 0) {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) {
                if (zoom > maxZoomIn) {
                    zoom -= zoomIncrease;
                    if (zoom < maxZoomIn) {
                        zoom = maxZoomIn;
                    }
                    Zoom();
                }
            } else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
                if (zoom < maxZoomOut) {
                    zoom += zoomIncrease;
                    if (zoom > maxZoomOut) {
                        zoom = maxZoomOut;
                    }
                    Zoom();
                }
            }
        }
    }

    void Zoom() {
        if (Manager.staticMonitor == true) {
        monitorCam.fieldOfView = zoom;
        }
        Manager.cam.fieldOfView = zoom;
    }
}
