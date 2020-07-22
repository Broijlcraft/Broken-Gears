using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScript : MonoBehaviour {

    public float zoom;
    public float zoomIncrease;
    public float maxZoomIn;
    public float maxZoomOut;
    public LayerMask layerMask;
    
    Camera monitorCam;

    private void Start() {
        OldManager.cam = GetComponent<Camera>();
        if (OldManager.staticMonitor == true) {
            monitorCam = transform.GetChild(0).GetComponent<Camera>();
        }
        if (zoom == 0) {
            zoom = OldManager.cam.fieldOfView;
        } else {
            OldManager.cam.fieldOfView = zoom;
            if (OldManager.staticMonitor == true) {
                monitorCam.fieldOfView = zoom;
            }
        }
    }

    private void Update() {
        if (Time.timeScale != 0 && MenuManager.mm_Single.currentMenuState == MenuManager.MenuState.Closed && PlayerLook.canMove == true) {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) {
                if (zoom > maxZoomIn) {
                    zoom -= zoomIncrease;
                    if (zoom < maxZoomIn) {
                        zoom = maxZoomIn;
                    }
                }
            } else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
                if (zoom < maxZoomOut) {
                    zoom += zoomIncrease;
                    if (zoom > maxZoomOut) {
                        zoom = maxZoomOut;
                    }
                }
            }
            Zoom();
        }
    }

    void Zoom() {
        if (OldManager.staticMonitor == true) {
        monitorCam.fieldOfView = zoom;
        }
        OldManager.cam.fieldOfView = zoom;
    }
}
