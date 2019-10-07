using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTowerPlacement : MonoBehaviour {

    public RaycastHit hit;
    public LayerMask layerMask;
    Vector3 pos;
    int i;
    public Tile tile;

    private void Update() {
        if (TowerManager.selectedTower == gameObject && Time.timeScale != 0) {
            Ray ray = Manager.cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, layerMask, 1000)) {
                tile = hit.transform.GetComponent<Tile>();

                if (hit.transform.GetComponent<Tile>().buildableParent == null) {
                    SetPos();
                    //if (tile.buildable == )
                } else {
                    pos = hit.transform.GetComponent<Tile>().buildableParent.position;
                }

                //float x = Mathf.Round(hit.point.x);
                //float y = Mathf.Round(hit.point.y);
                //float z = Mathf.Round(hit.point.z);
                //pos = new Vector3 (x, y, z);
                //if (Input.GetMouseButtonDown(0)) {
                //    if (tile.buildable == true) {
                //        TowerManager.selectedTower = null;
                //        tile.buildable = false;
                //    }
                //}
            }
            transform.position = pos;

            //Vector3 newRot = transform.rotation.eulerAngles;
            //float rot = Mathf.Clamp(Input.GetAxis("Mouse ScrollWheel") * 10, -1, 1);
            //if (t > 0 || t < 0) {
            //    newRot.y += 90f * rot;
            //}
            //transform.eulerAngles = newRot;

            if (Input.GetMouseButtonDown(1)) {
                TowerManager.selectedTower = null;
                Destroy(gameObject);
            }
        }
    }

    void SetPos() {
        float x = Mathf.Round(hit.point.x);
        float y = Mathf.Round(hit.point.y);
        float z = Mathf.Round(hit.point.z);
        pos = new Vector3(x, y, z);
    }
}
