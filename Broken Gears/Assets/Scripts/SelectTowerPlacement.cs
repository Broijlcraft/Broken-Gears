using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTowerPlacement : MonoBehaviour {

    public RaycastHit hit;
    public LayerMask layerMask;
    Vector3 pos;
    int i;

    private void Update() {
        if (TowerManager.selectedTower == gameObject) {
            Ray ray = Manager.cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, layerMask, 1000)) {
                float x = Mathf.Round(hit.point.x);
                float y = Mathf.Round(hit.point.y);
                float z = Mathf.Round(hit.point.z);
                pos = new Vector3 (x, y, z);
                if (Input.GetMouseButtonUp(0)) {
                    if (hit.transform.GetComponent<Tile>().buildable == true) {
                        TowerManager.selectedTower = null;
                        hit.transform.GetComponent<Tile>().buildable = false;
                    }
                }
            }

            Vector3 newRot = transform.rotation.eulerAngles;
            float t = Mathf.Clamp(Input.GetAxis("Mouse ScrollWheel") * 10, -1, 1);
            if (t > 0 || t < 0) {
            newRot.y += 90f * t;
            }
            transform.eulerAngles = newRot;
            transform.position = pos;
        }

        if (Input.GetMouseButtonDown(1) && TowerManager.selectedTower == gameObject) {
            TowerManager.selectedTower = null;
            Destroy(gameObject);
        }
    }
}
