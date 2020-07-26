using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldSelectTowerPlacement : MonoBehaviour {

    public int scrapCost, salvageValue;
    public LayerMask layerMask;
    public OldTurret turret;
    RaycastHit hit;
    Vector3 pos;
    Tile tile, childTile, parentTile;
    Vector3 newRot;

    private void Start() {
        //OldUiManager.old_um_Single.turretText.text = "";
    }

    private void Update() {
        if (OldTowerManager.old_tm_Single.selectedTower == gameObject && Time.timeScale != 0 /*&& OldUiManager.old_um_Single.gameOver == false*/) {
            Ray ray = OldManager.old_m_Single.cam.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(1)) {
                OldTowerManager.old_tm_Single.selectedTower = null;
                Destroy(gameObject);
            }
            if (OldTowerManager.old_tm_Single.selectedTower == gameObject) {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                    tile = hit.transform.GetComponent<Tile>();
                    if (tile) {
                        if (tile.buildable == true) {
                            ChangeColor(OldTowerManager.old_tm_Single.canPlaceColor);
                            if (tile.buildableParent != null) {
                                setPos(tile.buildableParent.GetComponent<Tile>().setPosition);
                                newRot = tile.buildableParent.GetComponent<Tile>().setRotation;
                            } else {
                                setPos(tile.setPosition);
                                newRot = tile.setRotation;  
                            }
                            if (Input.GetMouseButtonDown(0)) {
                                if (OldManager.old_m_Single.devMode == false) {
                                    OldScrapEconomy.old_se_Single.RemoveScrap(scrapCost);
                                }
                                transform.GetComponent<OldTurret>().coll.SetActive(true);
                                OldTowerManager.old_tm_Single.selectedTower = null;
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
        OldScrapEconomy.old_se_Single.AddScrap(salvageValue);
        OldMenuScript.old_ms_Single.MenuSwitch("none");
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
