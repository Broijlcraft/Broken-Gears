using UnityEngine;

public class Saw : MonoBehaviour {

    [SerializeField] private SawTower tower;
    [SerializeField] private LayerMask ignoreLayers;

    private bool overlap = false, lastOverlap = false;

    private void Start() {
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
        print(other.gameObject.layer);
        if (tower && other.gameObject.layer != ignoreLayers) {
            print("Overlap");
            overlap = true;
        }
    }
}
