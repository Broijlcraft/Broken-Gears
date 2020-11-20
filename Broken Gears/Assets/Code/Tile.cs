using UnityEngine;

public class Tile : MonoBehaviour {

    public Tile buildableParent;

    [HideInInspector] public bool buildable;
    [HideInInspector] public Tile buildableChild;
    [HideInInspector] public Vector3 setPosition, setRotation;

    private TowerManager tManager;

    private void Awake() {
        if (buildableParent != null) {
            setPosition = buildableParent.transform.position;
            buildable = true;
            buildableParent.buildableChild = this;
            buildableParent.buildable = true;
        } else {
            setPosition = transform.position;
        }
    }

    private void Start() {
        tManager = TowerManager.singleTM;
        if (buildableParent != null) {
            Vector3 newRot = tManager.GetTowerRotation(buildableParent.transform, transform);
            SetParentRotation(newRot);
        }    
    }

    void SetParentRotation(Vector3 rot) {
        buildableParent.setRotation = rot;
    }
}