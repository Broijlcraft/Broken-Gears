﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    public Transform targetPoint;
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
        waypoints = GameObject.Find("Waypoints").GetComponent<Waypoints>();
        SetTarget();
    }

    private void Update() {
        Vector3 direction =  abs(targetPoint.position, transform.position);
        //print(direction);
        Vector3 test = targetPoint.position - transform.position;
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
        if (Waypoints.waypoint.Length > targetValue) {
            targetPoint = Waypoints.waypoint[targetValue];
            enemyChild.transform.LookAt(targetPoint);
            enemyChild.transform.rotation = Quaternion.Euler(0, enemyChild.transform.localRotation.eulerAngles.y, 0);
            targetValue++;
        } else {
            speed = 0;
        }
    }
}
