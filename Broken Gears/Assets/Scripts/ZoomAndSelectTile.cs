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
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            if (zoom < maxZoomOut) {
                zoom += zoomIncrease;
                cam.fieldOfView = zoom;
            }
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit, 1000, layerMask)) {
                Instantiate(g, new Vector3(hit.transform.position.x, hit.transform.position.y + 0.5f, hit.transform.position.z), Quaternion.identity);
            }
        }
    }
}
