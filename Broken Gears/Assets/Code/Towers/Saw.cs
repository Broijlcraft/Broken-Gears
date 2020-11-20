using UnityEngine;

public class Saw : MonoBehaviour {

    private SawTower tower;
    private LayerMask ignoreLayers;

    private void Awake() {
        tower = GetComponentInParent<SawTower>();
        ignoreLayers = TowerManager.singleTM.GetIgnoreLayers();
    }

    private void OnTriggerStay(Collider other) {
        if (tower && other.gameObject.layer != ignoreLayers) {
            tower.OnTriggerEnterAndExit(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (tower && other.gameObject.layer != ignoreLayers) {
            tower.OnTriggerEnterAndExit(false);
        }
    }
}
