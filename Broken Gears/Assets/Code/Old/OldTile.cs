using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldTile : MonoBehaviour {

    [HideInInspector] public bool buildable;
    public Transform buildableParent;
    [HideInInspector] public Transform child;
    [HideInInspector] public Vector3 setPosition;
    [HideInInspector] public Vector3 setRotation;
    OldTile parentTile;

    private void Awake() {
        if (buildableParent != null) {
            parentTile = buildableParent.GetComponent<OldTile>();
            setPosition = buildableParent.transform.position;
            buildable = true;
            parentTile.child = transform;
            parentTile.buildable = true;
        } else {
            setPosition = transform.position;
        }
    }

    private void Start() {
        if (buildableParent != null && !GameManager.gm_Single.rework) { 
            if (buildableParent.position.x == transform.position.x) {
                if (buildableParent.position.z > transform.position.z) {
                    SetParentRotation(OldTowerManager.old_tm_Single.minZRotation);
                } else {
                    SetParentRotation(OldTowerManager.old_tm_Single.plusZRotation);
                }
            } else if (buildableParent.position.z == transform.position.z) {
                if (buildableParent.position.x > transform.position.x) {
                    SetParentRotation(OldTowerManager.old_tm_Single.plusXRotation);
                } else {
                    SetParentRotation(OldTowerManager.old_tm_Single.minXRotation);
                }
            }
        }    
    }

    void SetParentRotation(Vector3 v) {
        setRotation = v;
        parentTile.setRotation = setRotation;
    }
}
