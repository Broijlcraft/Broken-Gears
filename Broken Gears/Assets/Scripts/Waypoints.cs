using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {

    public GameObject[] waypoints;
    public static GameObject[] waypoint;
    public static string fString;
    public static string sString;
    //public List<Transform> wp = new List<Transform>();

    private void Awake() {
        //waypoint = waypoints;
        waypoint = GameObject.FindGameObjectsWithTag("Waypoint");
    }
}
