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
        if (Time.timeScale == 1) {
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

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && Time.timeScale == 1) {
            if (Physics.Raycast(ray, out hit, 1000, layerMask)) {
                if (hit.transform.GetComponent<Tile>().buildable == true) {
                    Instantiate(g, new Vector3(hit.transform.position.x, hit.transform.position.y + 0.5f, hit.transform.position.z), Quaternion.identity);
                    hit.transform.GetComponent<Tile>().PlaceTower();
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
