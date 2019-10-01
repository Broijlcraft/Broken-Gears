using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Waypoints : MonoBehaviour {

    public Transform[] waypoints;
    public static Transform[] waypoint;

    private void Awake() {
        waypoint = waypoints;
    }
}
