using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndSelectTile : MonoBehaviour {

    public float zoom;
    public float zoomIncrease;
    public float maxZoomIn;
    public float maxZoomOut;
    public LayerMask layerMask;
    public RaycastHit hit;
    public GameObject g;
    
    Camera cam;

    Camera monitorCam;

    private void Start() {
        cam = GetComponent<Camera>();
        if (Manager.staticMonitor == true) {
            monitorCam = transform.GetChild(0).GetComponent<Camera>();
        }
        if (zoom == 0) {
            zoom = cam.fieldOfView;
        } else {
            cam.fieldOfView = zoom;
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

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0) && TowerManager.selectedTower != null) {
                if (Physics.Raycast(ray, out hit, 1000)) {
                    if (hit.transform.tag == "Tile") {
                        Tile tile = hit.transform.GetComponent<Tile>();
                        if (tile.buildable == true) {
                            if (tile.buildableParent == null) {
                                tile.PlaceTower(TowerManager.selectedTower);
                            } else {
                                tile.buildableParent.GetComponent<Tile>().PlaceTower(TowerManager.selectedTower);
                            }
                            TowerManager.selectedTower = null;
                        }
                    }
                }
            }
        }
    }

    void Zoom() {
        if (Manager.staticMonitor == true) {
        monitorCam.fieldOfView = zoom;
        }
        cam.fieldOfView = zoom;
    }
}
