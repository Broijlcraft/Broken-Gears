namespace BrokenGears.Pathing {
    using UnityEngine;
    using System.Collections.Generic;

    public class WaypointsManager : MonoBehaviour {

        [SerializeField] private Transform[] waypoints;

        public static WaypointsManager Instance { get; private set; }
        public Queue<Transform> Waypoints { get; private set; }

        private void Awake() {
            Instance = this;

            for (int i = 0; i < waypoints.Length; i++) {
                Waypoints.Enqueue(waypoints[i]);
            }
        }
    }
}