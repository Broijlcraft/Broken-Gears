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
        if (enemyChild == null) {
            transform.GetChild(0);
        }
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
            enemyChild.transform.rotation = Quaternion.Euler(0, enemyChild.transform.localRotation.eulerAngles.y, 0);
            targetValue++;
        } else {
            speed = 0;
            //start doorbanging animation
        }
    }
}
