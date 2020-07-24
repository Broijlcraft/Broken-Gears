using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldZoomScript : MonoBehaviour {

    public static OldZoomScript old_zs_Single;

    public float zoom, zoomIncrease, maxZoomIn, maxZoomOut;
    public LayerMask layerMask;
    
    Camera monitorCam;

    private void Awake() {
        old_zs_Single = this;
    }

    private void Start() {
        OldManager.old_m_Single.cam = GetComponent<Camera>();
        if (OldManager.old_m_Single.monitor == true) {
            monitorCam = transform.GetChild(0).GetComponent<Camera>();
        }
        if (zoom == 0) {
            zoom = OldManager.old_m_Single.cam.fieldOfView;
        } else {
            OldManager.old_m_Single.cam.fieldOfView = zoom;
            if (OldManager.old_m_Single.monitor == true) {
                monitorCam.fieldOfView = zoom;
            }
        }
    }

    private void Update() {
        if (Time.timeScale != 0 && MenuManager.mm_Single.currentMenuState == MenuManager.MenuState.Closed && OldPlayerLook.old_pl_single.canMove == true) {
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
        if (OldManager.old_m_Single.monitor == true) {
        monitorCam.fieldOfView = zoom;
        }
        OldManager.old_m_Single.cam.fieldOfView = zoom;
    }
}
