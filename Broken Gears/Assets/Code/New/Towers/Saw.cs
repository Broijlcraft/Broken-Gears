using UnityEngine;

public class Saw : MonoBehaviour {

    SawTower tower;

    private void Start() {
        tower = GetComponentInParent<SawTower>();
    }

    private void OnTriggerStay(Collider other) {
        if (tower && other.gameObject.layer != TowerManager.tm_Single.layersToIgnoreWhenAttacking) {
            tower.OnTriggerEnterAndExit(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (tower && other.gameObject.layer != TowerManager.tm_Single.layersToIgnoreWhenAttacking) {
            tower.OnTriggerEnterAndExit(false);
        }
    }
}
