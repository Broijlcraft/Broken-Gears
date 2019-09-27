using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {

    public GameObject[] waypoints;
    public static GameObject[] waypoint;

    private void Awake() {
        waypoint = waypoints;
    }
}
