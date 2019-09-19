using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform target;

    public GameObject barrel;
    public float range;

    private void Start() {
        UpdateTarget();
    }

    void UpdateTarget() {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("EnemyTarget");
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
