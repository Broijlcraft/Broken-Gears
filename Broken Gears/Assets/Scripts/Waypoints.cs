using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {

    public static GameObject[] waypoint;

    private void Awake() {
        waypoint = GameObject.FindGameObjectsWithTag("Waypoint");
    }
}
