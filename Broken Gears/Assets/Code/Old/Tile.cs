using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [HideInInspector] public bool buildable;
    public Transform buildableParent;
    [HideInInspector] public Transform child;
    [HideInInspector] public Vector3 setPosition;
    [HideInInspector] public Vector3 setRotation;
    Tile parentTile;

    private void Awake() {
        if (buildableParent != null) {
            parentTile = buildableParent.GetComponent<Tile>();
            setPosition = buildableParent.transform.position;
            buildable = true;
            parentTile.child = transform;
            parentTile.buildable = true;
        } else {
            setPosition = transform.position;
        }
    }

    private void Start() {
        if (buildableParent != null) { 
            if (buildableParent.position.x == transform.position.x) {
                if (buildableParent.position.z > transform.position.z) {
                    SetParentRotation(TowerManager.minZRotation);
                } else {
                    SetParentRotation(TowerManager.plusZRotation);
                }
            } else if (buildableParent.position.z == transform.position.z) {
                if (buildableParent.position.x > transform.position.x) {
                    SetParentRotation(TowerManager.plusXRotation);
                } else {
                    SetParentRotation(TowerManager.minXRotation);
                }
            }
        }    
    }

    void SetParentRotation(Vector3 v) {
        setRotation = v;
        parentTile.setRotation = setRotation;
    }
}
