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
        if (buildableParent != null) { 
            if (buildableParent.transform.position.x == transform.position.x) {
                if (buildableParent.transform.position.z > transform.position.z) {
                    SetParentRotation(TowerManager.tm_Single.towerRotations.minZRotation);
                } else {
                    SetParentRotation(TowerManager.tm_Single.towerRotations.plusZRotation);
                }
            } else if (buildableParent.transform.position.z == transform.position.z) {
                if (buildableParent.transform.position.x > transform.position.x) {
                    SetParentRotation(TowerManager.tm_Single.towerRotations.plusXRotation);
                } else {
                    SetParentRotation(TowerManager.tm_Single.towerRotations.minXRotation);
                }
            }
        }    
    }

    void SetParentRotation(Vector3 rot) {
        buildableParent.setRotation = rot;
    }
}