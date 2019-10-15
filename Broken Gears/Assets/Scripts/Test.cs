using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Test : MonoBehaviour {

    public Transform target;
    public NavMeshAgent agent;
    public float maxDistance;
    int targetValue;

    private void Start() {
        SetTarget();
    }

    private void Update() {
        Vector3 direction = abs(agent.destination, transform.position);
        if (direction.z < maxDistance && direction.x < maxDistance) {
            SetTarget();
        }
        //rigid.velocity = Vector3.zero;
    }

    Vector3 abs(Vector3 v, Vector3 vA) {
        Vector3 vB = v - vA;
        float x = Mathf.Abs(vB.x);
        float y = Mathf.Abs(vB.y);
        float z = Mathf.Abs(vB.z);
        return new Vector3(x, y, z);
    }

    //
    public void SetTarget() {
        if (Waypoints.waypoint.Length > targetValue) {
            agent.SetDestination(Waypoints.waypoint[targetValue].position);
            target = Waypoints.waypoint[targetValue];
            targetValue++;
        }
    }
}
