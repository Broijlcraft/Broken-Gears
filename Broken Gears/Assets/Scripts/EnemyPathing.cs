using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    public GameObject targetPoint;
    public float speed;
    public int targetValue;
    public float maxDistance;

    private void Start() {
        SetTarget(targetValue);
    }

    private void Update() {
        Vector3 direction = targetPoint.transform.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);
        float f = Vector3.Distance(targetPoint.transform.position, transform.position);
        print(direction);
        if (direction.z <= maxDistance) {
            SetTarget(targetValue);
        }
        
    }

    void SetTarget(int next) {
        if (Waypoints.waypoint.Length > next) {
            targetPoint = Waypoints.waypoint[next];
            targetValue++;
        } else {
            speed = 0;
            //start doorbanging animation
        }
    }
}
