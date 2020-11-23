using UnityEngine;

public class Saw : MonoBehaviour {

    private SawTower tower;
    private LayerMask ignoreLayers;

    private bool overlap = false, lastOverlap = false;

    private void Awake() {
        tower = GetComponentInParent<SawTower>();
        ignoreLayers = TowerManager.singleTM.GetIgnoreLayers();
    }

    private void FixedUpdate() {
        if(overlap != lastOverlap) {
            tower.OnTriggerEnterAndExit(overlap);
            lastOverlap = overlap;
        }
        overlap = false;
    }

    private void OnTriggerStay(Collider other) {
        if (tower && other.gameObject.layer != ignoreLayers) {
            overlap = true;
        }
    }
}
