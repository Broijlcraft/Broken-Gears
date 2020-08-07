using UnityEngine;

public class TowerManager : MonoBehaviour {
    public static TowerManager tm_Single;
    public LayerMask layersToIgnoreWhenAttacking = (1 << 8), tileMask;
    public Color canPlaceColor, canNotPlaceColor;

    public TowerRotations towerRotations;

    [Header("HideInInspector")]
    public Tower selectedTower;
    Ray ray;

    private void Awake() {
        tm_Single = this;
    }

    public void PickTower(Tower pickedTower) {
        Tower tower = Instantiate(pickedTower, Vector3.zero, Quaternion.identity);
        SelectTower(tower);
    }

    public void SelectTower(Tower tower) {
        RemoveTower(selectedTower, true);
        selectedTower = tower;
    }

    public void Update() {
        ray = Movement.m_Single.topdownCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(1)) {
            RemoveTower(selectedTower, false);
        }
        MoveTower();
    }

    void MoveTower() {
        if (selectedTower) {
            if (!selectedTower.placedOnParentTile) {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileMask)) {
                    Vector3 newPos = hit.transform.position;
                    Vector3 newRot = hit.transform.rotation.eulerAngles;
                    Tile tile = hit.transform.GetComponent<Tile>();
                    if (tile) {
                        if (tile.buildable == true) {
                            selectedTower.ChangeTowerColor(canPlaceColor, true);
                            if (tile.buildableParent == null) {
                                newPos = tile.transform.position;
                                newRot = tile.setRotation;
                            } else {
                                newPos = tile.buildableParent.transform.position;
                                newRot = tile.buildableParent.setRotation;
                            }
                            //if (Input.GetMouseButtonDown(0)) {
                            //    if (OldManager.old_m_Single.devMode == false) {
                            //        //OldScrapEconomy.old_se_Single.RemoveScrap(scrapCost);
                            //    }
                            //    //transform.GetComponent<OldTurret>().coll.SetActive(true);
                            //    OldTowerManager.old_tm_Single.selectedTower = null;
                            //    tile.buildable = false;
                            //    if (tile.buildableParent != null) {
                            //        tile.buildableParent.GetComponent<Tile>().buildable = false;
                            //        parentTile = tile.buildableParent.GetComponent<Tile>();
                            //    } else {
                            //        tile.child.GetComponent<Tile>().buildable = false;
                            //        childTile = tile.child.GetComponent<Tile>();
                            //    }
                            //    selectedTower.ChangeTowerColor(Color.white, false);
                            //}
                        } else {
                            newPos = tile.setPosition;
                            selectedTower.ChangeTowerColor(canNotPlaceColor, true); ;
                        }
                    }
                    selectedTower.transform.eulerAngles = newRot;
                    selectedTower.transform.position = newPos;
                }
            }
        }
    }

    void RemoveTower(Tower tower, bool destroyAlways) {
        print("remove tower");
        if (tower) {
            print("remove tower " + tower);
            if (selectedTower.oldParentTile && !destroyAlways) {
                selectedTower.PlaceOnTile(selectedTower.oldParentTile);
                print("Reset to tile");
            } else {
                Destroy(selectedTower.gameObject);
                selectedTower = null;
                print("destroyed");
            }
        }
    }
}

[System.Serializable]
public class TowerRotations {
    public Vector3 plusXRotation, minXRotation, plusZRotation, minZRotation;
}