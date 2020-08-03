using UnityEngine;

public class Tools : MonoBehaviour{

    public static Tools tools;

    private void Awake() {
        tools = this;
    }

    public void EnableDisableGameObjectsFromArray(GameObject[] go, bool newState) {
        for (int i = 0; i < go.Length; i++) {
            if (go[i]) {
                go[i].SetActive(newState);
            }
        }
    }

    public void StartStopParticleSystemsFromArray(ParticleSystem[] systems, bool start) {
        for (int i = 0; i < systems.Length; i++) {
            if (start) {
                systems[i].Play();
            } else {
                systems[i].Stop();
            }
        }
    }

    public Transform GetTarget(Enemy enemy) {
        Transform tp = enemy.transform;
        if (enemy.targetingPoint) {
            tp = enemy.targetingPoint;
        }
        return tp;
    }
}