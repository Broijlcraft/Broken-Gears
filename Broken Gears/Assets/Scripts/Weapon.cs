using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform target;
    public Transform defaultTarget;
    public Mesh mesh;
    public Material mat;

    public GameObject barrel;
    public float range;

    public List<GameObject> targetsInRange = new List<GameObject>();

    private void Start() {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
    }

    void UpdateTarget() {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("EnemyTarget");

        for (int i = 0; i < targets.Length; i++) {
            if (distance(transform.position, targets[i].transform.position) < range && !targetsInRange.Contains(targets[i])) {
                targetsInRange.Add(targets[i]);
            } else if (targetsInRange.Contains(targets[i]) && distance(transform.position, targets[i].transform.position) > range) {
                targetsInRange.Remove(targets[i]);
            }
        }

        if (targetsInRange.Count > 0) {
            if (targetsInRange[0] != null) {
                target = targetsInRange[0].transform;
            } else {
                targetsInRange.RemoveAt(0);
            }
        } else {
            target = defaultTarget;
        }
    }

    float distance(Vector3 vA, Vector3 vB) {
        return Vector3.Distance(vA, vB);
    }

    private void OnDrawGizmos() {
        Gizmos.color = mat.color;
        Gizmos.DrawMesh(mesh, transform.position, Quaternion.identity, new Vector3(range - 0.5f, range - 0.5f, range - 0.5f));
    }
}
