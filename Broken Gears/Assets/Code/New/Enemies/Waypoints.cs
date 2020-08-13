using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Waypoints : MonoBehaviour {

    public static Waypoints wp_Single;

    [HideInInspector] public List<Transform> waypoints = new List<Transform>();

    private void Awake() {
        wp_Single = this;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Waypoints))]
public class WaypointEditor : Editor {
    Waypoints waypointsScript;

    private void OnEnable() {
        waypointsScript = (Waypoints)target;
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if (GUILayout.Button("Set waypoints (from top to bottom)")) {
            List<Transform> waypointsList = new List<Transform>();
            for (int i = 0; i < waypointsScript.transform.childCount; i++) {
                waypointsList.Add(waypointsScript.transform.GetChild(i));
                EditorUtility.SetDirty(waypointsList[i]);
            }
            waypointsScript.waypoints = waypointsList;
            Debug.LogWarning("Successfully set waypoints, don't forget to save!");
        }
    }
}
#endif