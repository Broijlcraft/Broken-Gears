using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTowerPlacement : MonoBehaviour {

    public int scrapValue;
    public LayerMask layerMask;
    RaycastHit hit;
    Vector3 pos;
    int i;
    Tile tile;
    Vector3 newRot;

    private void Update() {
        if (TowerManager.selectedTower == gameObject && Time.timeScale != 0) {
            Ray ray = Manager.cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, layerMask, 1000)) {
                tile = hit.transform.GetComponent<Tile>();
                if (tile.buildable == true) {
                    if (tile.buildableParent != null) {
                        setPos(tile.buildableParent.GetComponent<Tile>().setPosition);
                        newRot = tile.buildableParent.GetComponent<Tile>().setRotation;
                        print("comp");
                    } else {
                        setPos(tile.setPosition);
                        newRot = tile.setRotation;
                        print("else");
                    }
                    if (Input.GetMouseButtonDown(0)) {
                        if (tile.buildable == true) {
                            transform.GetComponent<Turret>().coll.SetActive(true);
                            TowerManager.selectedTower = null;
                            tile.buildable = false;
                            if (tile.buildableParent != null) {
                                tile.buildableParent.GetComponent<Tile>().buildable = false;
                            }
                        }
                    }
                } else {
                    setPos(tile.setPosition);
                }
            }

            transform.eulerAngles = newRot;
            transform.position = pos;

            if (Input.GetMouseButtonDown(1)) {
                TowerManager.selectedTower = null;
                Destroy(gameObject);
            }
        }
    }
    
    Vector3 setPos(Vector3 v) {
        float x = Mathf.Round(v.x);
        float y = Mathf.Round(v.y);
        float z = Mathf.Round(v.z);
        return pos = new Vector3(x, y, z);
    }
}
