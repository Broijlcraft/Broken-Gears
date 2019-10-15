using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    public Transform targetPoint;
    public float speed;
    public float rotationSpeed;
    int targetValue = 0;
    public float maxDistance;

    public Text text;

    Rigidbody rigid;
    public GameObject enemyChild;
    public Vector3 test;
    public int b;
    public int bb;

    private void Start() {
        rigid = transform.GetComponent<Rigidbody>();
        SetTarget();
    }

    private void Update() {
        Vector3 direction = abs(targetPoint.position, transform.position);
        Vector3 test = targetPoint.position - transform.position;
        transform.Translate(test.normalized * speed * Time.deltaTime);
        if (direction.z < maxDistance && direction.x < maxDistance) {
            SetTarget();
        }
        Vector3 armDir = targetPoint.position - enemyChild.transform.position;
        Quaternion armLookRotation = Quaternion.LookRotation(armDir);
        Vector3 armRotation = Quaternion.Lerp(enemyChild.transform.rotation, armLookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        enemyChild.transform.rotation = Quaternion.Euler(0f, armRotation.y, 0f);
        rigid.velocity = Vector3.zero;
    }

    void Test() {
        print("Test");
    }

    Vector3 abs(Vector3 v, Vector3 vA) {
        Vector3 vB = v - vA;
        float x = Mathf.Abs(vB.x);
        float y = Mathf.Abs(vB.y);
        float z = Mathf.Abs(vB.z);
        return new Vector3(x, y, z);
    }

    public void SetTarget() {
        if (Waypoints.waypoint.Length > targetValue) {
            targetPoint = Waypoints.waypoint[targetValue];
            targetValue++;
        } else {
            speed = 0;
            rotationSpeed = 0;
        }
    }
}
