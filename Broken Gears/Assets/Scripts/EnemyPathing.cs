using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    public GameObject targetPoint;
    public float speed;
    public int targetValue;
    public float maxDistance;
    bool turning;
    float turnTimer;
    public float maxRotateTime;

    public GameObject enemyChild;

    private void Start() {
        SetTarget();
    }

    private void Update() {
        Vector3 direction = targetPoint.transform.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);
       
        if (direction.z <= maxDistance) {
            SetTarget();
        }  
    }

    void SetTarget() {
        if (Waypoints.waypoint.Length > targetValue) {
            targetPoint = Waypoints.waypoint[targetValue];
            enemyChild.transform.LookAt(targetPoint.transform);
            targetValue++;
        } else {
            speed = 0;
            //start doorbanging animation
        }
    }
}
