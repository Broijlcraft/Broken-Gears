using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    public GameObject targetPoint;
    public GameObject secondTarget;
    public float speed;
    int targetValue = 0;
    public float maxDistance;
    bool turning;
    float turnTimer;
    public float maxRotateTime;

    public Text text;

    public Waypoints waypoints;

    public GameObject enemyChild;
    bool b;

    private void Start() {
        text = GameObject.Find("Canvas").transform.Find("Debug").GetComponent<Text>();
        waypoints = GameObject.Find("Waypoints").GetComponent<Waypoints>();
        SetTarget();
    }

    private void Update() {
        Vector3 direction =  abs(targetPoint.transform.position, transform.position);
        print(direction);
        Vector3 test = targetPoint.transform.position - transform.position;
        transform.Translate(test.normalized * speed * Time.deltaTime);
        if (direction.z < maxDistance && direction.x < maxDistance) {
            SetTarget();
        }  
    }

    Vector3 abs(Vector3 v, Vector3 vA) {
        Vector3 vB = v - vA;
        float x = Mathf.Abs(vB.x);
        float y = Mathf.Abs(vB.y);
        float z = Mathf.Abs(vB.z);
        return new Vector3(x, y, z);
    }

    //
    public void SetTarget() {
        print(targetValue);
        if (Waypoints.waypoint.Length > targetValue) {
            text.text = text.text + ("|length");
            targetPoint = Waypoints.waypoint[targetValue];
            enemyChild.transform.LookAt(targetPoint.transform);
            enemyChild.transform.rotation = Quaternion.Euler(0, enemyChild.transform.localRotation.eulerAngles.y, 0);
            targetValue++;
        } else {
            speed = 0;
            //attack animation
        }


        //if (waypoints.waypoint.Length > targetValue) {
        //    text.text = text.text + ("|length");
        //    targetPoint = waypoints.waypoint[targetValue];
        //    enemyChild.transform.LookAt(targetPoint.transform);
        //    enemyChild.transform.rotation = Quaternion.Euler(0, enemyChild.transform.localRotation.eulerAngles.y, 0);
        //    targetValue++;
        //} else {
        //    speed = 0;
        //    //attack animation
        //}
    }
}
