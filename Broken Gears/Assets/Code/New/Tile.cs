using UnityEngine;

public class Tile : MonoBehaviour {

    public Tile buildableParent;

    [HideInInspector] public bool buildable;
    [HideInInspector] public Transform child;
    [HideInInspector] public Vector3 setPosition, setRotation;

    private void Awake() {
        if (buildableParent != null) {
            setPosition = buildableParent.transform.position;
            buildable = true;
            buildableParent.child = transform;
            buildableParent.buildable = true;
        } else {
            setPosition = transform.position;
        }
    }

    private void Start() {
        if (buildableParent != null && !GameManager.gm_Single.rework) { 
            if (buildableParent.transform.position.x == transform.position.x) {
                if (buildableParent.transform.position.z > transform.position.z) {
                    SetParentRotation(OldTowerManager.old_tm_Single.minZRotation);
                } else {
                    SetParentRotation(OldTowerManager.old_tm_Single.plusZRotation);
                }
            } else if (buildableParent.transform.position.z == transform.position.z) {
                if (buildableParent.transform.position.x > transform.position.x) {
                    SetParentRotation(OldTowerManager.old_tm_Single.plusXRotation);
                } else {
                    SetParentRotation(OldTowerManager.old_tm_Single.minXRotation);
                }
            }
        }    
    }

    void SetParentRotation(Vector3 rot) {
        buildableParent.setRotation = rot;
    }
}