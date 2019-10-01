using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Waypoints : MonoBehaviour {

    public GameObject[] waypoints;
    public static GameObject[] waypoint;

    public Text debug;

    public List<GameObject> test_1 = new List<GameObject>();
    public GameObject[] test_2;

    private void Awake() {
        waypoint = waypoints;
        for (int i = 0; i < transform.childCount; i++) {
            test_1.Add(transform.GetChild(i).gameObject);
        }

        test_2 = GameObject.FindGameObjectsWithTag("Waypoint");

        debug.text = debug.text + "_TEST_1_";

        for (int ib = 0; ib < test_1.Count; ib++) {
            debug.text = debug.text + test_1[ib].name;
        }

        debug.text = debug.text + "_TEST_2_";

        for (int ic = 0; ic < test_2.Length; ic++) {
            debug.text = debug.text + test_1[ic].name;
        }
    }
}
