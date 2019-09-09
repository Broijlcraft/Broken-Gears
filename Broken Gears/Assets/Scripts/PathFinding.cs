using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

    GameObject startPos;
    GameObject endPos;
    GameObject[] waypoints;

    private void Start() {
        startPos = GameObject.Find("StartPoint");
        endPos = GameObject.Find("EndPoint");
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }
}
