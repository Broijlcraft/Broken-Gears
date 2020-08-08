using UnityEngine;

public class TowerManager : MonoBehaviour {
    public static TowerManager tm_Single;
    public LayerMask layersToIgnoreWhenAttacking = (1 << 8), tileMask;
    public string buildableTileTag = "BuildableTile";
    public Color canPlaceColor, canNotPlaceColor;

    public TowerRotations towerRotations;

    [Header("HideInInspector")]
    public Tower selectedTower;
    Ray ray;

    private void Awake() {
        tm_Single = this;
    }

    public void Update() {
        ray = Movement.m_Single.topdownCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(1)) {
            RemoveTower(selectedTower, false);
        }
        SelectedTowerAction();
    }

    public void PickTower(Tower pickedTower) {
        Tower tower = Instantiate(pickedTower, Vector3.zero, Quaternion.identity);
        SelectTower(tower);
    }

    public void SelectTower(Tower tower) {
        RemoveTower(selectedTower, true);
        selectedTower = tower;
    }

    void SelectedTowerAction() {
        if (selectedTower) {
            if (!selectedTower.placedOnParentTile) {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileMask)) {
                    Vector3 newPos = hit.transform.position;
                    Vector3 newRot = hit.transform.rotation.eulerAngles;
                    if (hit.transform.CompareTag(buildableTileTag)) {
                        Tile tile = hit.transform.GetComponent<Tile>();
                        if (tile) {
                            if (tile.buildable == true) {
                                selectedTower.ChangeTowerColor(canPlaceColor);
                                if (tile.buildableParent == null) {
                                    newPos = tile.transform.position;
                                    newRot = tile.setRotation;
                                } else {
                                    newPos = tile.buildableParent.transform.position;
                                    newRot = tile.buildableParent.setRotation;
                                }
                                if (Input.GetMouseButtonDown(0)) {
                                    if (GameManager.gm_Single.devMode == false) {
                                        ScrapManager.sm_single.AddOrWithdrawScrap(selectedTower.buyScrapPrice, ScrapManager.ScrapOption.Withdraw);
                                    }
                                    tile.buildable = false;
                                    if (tile.buildableParent != null) {
                                        tile.buildableParent.buildable = false;
                                        tile = tile.buildableParent;
                                    } else {
                                        tile.buildableChild.buildable = false; 
                                    }
                                    selectedTower.PlaceOnParentTile(tile);
                                }
                            } else {
                                newPos = tile.setPosition;
                                newRot = selectedTower.transform.rotation.eulerAngles;
                                selectedTower.ChangeTowerColor(canNotPlaceColor);
                            }
                        }
                    } else {
                        selectedTower.ChangeTowerColor(canNotPlaceColor);
                    }
                    if (selectedTower) {
                        selectedTower.transform.eulerAngles = newRot;
                        selectedTower.transform.position = newPos;
                    }
                }
            }
        }
    }

    void RemoveTower(Tower tower, bool destroyAlways) {
        print("remove tower");
        if (tower) {
            print("remove tower " + tower);
            if (selectedTower.oldParentTile && !destroyAlways) {
                selectedTower.PlaceOnParentTile(selectedTower.oldParentTile);
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