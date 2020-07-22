using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldSelectTowerPlacement : MonoBehaviour {

    public int scrapCost;
    public int salvageValue;
    public LayerMask layerMask;
    RaycastHit hit;
    Vector3 pos;
    OldTile tile;
    Vector3 newRot;
    public Turret turret;
    OldTile childTile;
    OldTile parentTile;

    private void Start() {
        OldUiManager.staticTurretText.text = "";
    }

    private void Update() {
        if (OldTowerManager.selectedTower == gameObject && Time.timeScale != 0 && OldUiManager.gameOver == false) {
            Ray ray = OldManager.cam.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(1)) {
                OldTowerManager.selectedTower = null;
                Destroy(gameObject);
            }
            if (OldTowerManager.selectedTower == gameObject) {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                    tile = hit.transform.GetComponent<OldTile>();
                    if (tile) {
                        if (tile.buildable == true) {
                            ChangeColor(OldTowerManager.old_tm_Single.canPlaceColor);
                            if (tile.buildableParent != null) {
                                setPos(tile.buildableParent.GetComponent<OldTile>().setPosition);
                                newRot = tile.buildableParent.GetComponent<OldTile>().setRotation;
                            } else {
                                setPos(tile.setPosition);
                                newRot = tile.setRotation;  
                            }
                            if (Input.GetMouseButtonDown(0)) {
                                if (OldManager.devMode == false) {
                                    ScrapEconomy.RemoveScrap(scrapCost);
                                }
                                transform.GetComponent<Turret>().coll.SetActive(true);
                                OldTowerManager.selectedTower = null;
                                tile.buildable = false;
                                if (tile.buildableParent != null) {
                                    tile.buildableParent.GetComponent<OldTile>().buildable = false;
                                    parentTile = tile.buildableParent.GetComponent<OldTile>();
                                } else {
                                    tile.child.GetComponent<OldTile>().buildable = false;
                                    childTile = tile.child.GetComponent<OldTile>();
                                }
                                ChangeColor(Vector4.zero);
                            }
                        } else {
                            setPos(tile.setPosition);
                            ChangeColor(OldTowerManager.old_tm_Single.canNotPlaceColor);
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
        OldUiManager.staticMenuScript.MenuSwitch("none");
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
