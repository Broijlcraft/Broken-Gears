namespace BrokenGears.Pathing {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PathFinding : MonoBehaviour {

        [SerializeField] private float movementSpeed = 1f;
        [SerializeField] private float rotationSpeed = 1f;
        [SerializeField] private Transform model;

        private Queue<Transform> waypoints;
        private Transform currentWaypoint;
        
        void Start() {
            TryGetWaypoints();
        }

        void Update() {
            if (currentWaypoint) {
                Vector3 direction = (currentWaypoint.position - transform.position).normalized;
                transform.Translate(direction * Time.deltaTime * movementSpeed);

                Quaternion lookRotation = Quaternion.LookRotation(direction);
                Vector3 rotationToLook = Quaternion.Lerp(model.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
                model.rotation = Quaternion.Euler(0f, rotationToLook.y, 0f);

                if (Vector3.Distance(transform.position, currentWaypoint.position) < .1f) {
                    SetNextWaypoint();
                }
            }
        }

        private void TryGetWaypoints() {
            if (WaypointsManager.Instance) {
                waypoints = WaypointsManager.Instance.Waypoints;
                SetNextWaypoint();
            }
        }

        private void SetNextWaypoint() {
            if (waypoints != null) {
                if(waypoints.Count != 0) {
                    currentWaypoint = waypoints.Dequeue();
                    return;
                }
                currentWaypoint = null;
            }
        }
    }
}