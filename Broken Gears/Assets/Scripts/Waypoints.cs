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
        //waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        waypoint = waypoints;
        //waypoint = GameObject.FindGameObjectsWithTag("Waypoint");
        //GetWayPoints();
    }


    //void GetWayPoints() {
    //    foreach(Transform t in transform) {
    //        if(t != transform) {
    //            wp.Add(t);
    //        }
    //    }
    //}
}
