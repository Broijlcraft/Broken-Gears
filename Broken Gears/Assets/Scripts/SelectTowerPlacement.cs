using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTowerPlacement : MonoBehaviour {

    public int scrapCost;
    public int salvageValue;
    public LayerMask layerMask;
    RaycastHit hit;
    Vector3 pos;
    Tile tile;
    Vector3 newRot;
    public Turret turret;
    Tile childTile;
    Tile parentTile;

    private void Start() {
        UiManager.staticTurretText.text = "";
    }

    private void Update() {
        if (TowerManager.selectedTower == gameObject && Time.timeScale != 0) {
            Ray ray = Manager.cam.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(1)) {
                TowerManager.selectedTower = null;
                Destroy(gameObject);
            }
            if (TowerManager.selectedTower == gameObject) {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                    tile = hit.transform.GetComponent<Tile>();
                    if (tile) {
                        if (tile.buildable == true) {
                            ChangeColor(TowerManager.canPlace);
                            if (tile.buildableParent != null) {
                                setPos(tile.buildableParent.GetComponent<Tile>().setPosition);
                                newRot = tile.buildableParent.GetComponent<Tile>().setRotation;
                            } else {
                                setPos(tile.setPosition);
                                newRot = tile.setRotation;  
                            }
                            if (Input.GetMouseButtonDown(0)) {
                                if (Manager.devMode == false) {
                                    ScrapEconomy.RemoveScrap(scrapCost);
                                }
                                transform.GetComponent<Turret>().coll.SetActive(true);
                                TowerManager.selectedTower = null;
                                tile.buildable = false;
                                if (tile.buildableParent != null) {
                                    tile.buildableParent.GetComponent<Tile>().buildable = false;
                                    parentTile = tile.buildableParent.GetComponent<Tile>();
                                } else {
                                    tile.child.GetComponent<Tile>().buildable = false;
                                    childTile = tile.child.GetComponent<Tile>();
                                }
                                ChangeColor(Vector4.zero);
                            }
                        } else {
                            setPos(tile.setPosition);
                            ChangeColor(TowerManager.canNotPlace);
                        }
                    }
                }
            }

            transform.eulerAngles = newRot;
            transform.position = pos;

        }
    }
    
    public void SellTower() {
        if (tile != null) {
            tile.buildable = true;
        }
        if (childTile != null) {
            childTile.buildable = true;
        }
        if (parentTile != null) {
            parentTile.buildable = true;
        }
        ScrapEconomy.AddScrap(salvageValue);
        UiManager.staticMenuScript.MenuSwitch("none");
        Destroy(gameObject);
    }

    void ChangeColor(Vector4 v) {
        for (int i = 0; i < turret.weaponParts.Count; i++) {
            turret.weaponParts[i].material.EnableKeyword("_EmissionColor");
            turret.weaponParts[i].material.SetColor("_EmissionColor", new Color(v.x, v.y, v.z, v.w));
        }
    }

    Vector3 setPos(Vector3 v) {
        float x = Mathf.Round(v.x);
        float y = Mathf.Round(v.y);
        float z = Mathf.Round(v.z);
        return pos = new Vector3(x, y, z);
    }
}
