using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomper : MonoBehaviour {
    public float attackSpeed;
    public float decentSpeed;
    public float ascentSpeed;
    float actualSpeed;
    public Transform target;
    public Transform targetPointUp;
    public Transform targetPointDown;
    public float maxDistance;

    bool b;

    private void Start() {
        actualSpeed = decentSpeed;
        target = targetPointDown;
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            b = true;
        }
        if (b == true) {
            Vector3 direction = abs(targetPointDown.localPosition, transform.localPosition);
            print(direction);
            Vector3 test = target.localPosition - transform.localPosition;
            if (direction.z < maxDistance && direction.x < maxDistance) {
                ChangeDirection();
            }
            transform.Translate(test.normalized * actualSpeed * Time.deltaTime);
        }
    }

    void ChangeDirection() {
        b = false;
        transform.localPosition = target.localPosition;
        if (target == targetPointDown) {
            target = targetPointUp;
            actualSpeed = ascentSpeed;
            print("Yeah");
        }
    }

    Vector3 abs(Vector3 v, Vector3 vA) {
        Vector3 vB = v - vA;
        float x = Mathf.Abs(vB.x);
        float y = Mathf.Abs(vB.y);
        float z = Mathf.Abs(vB.z);
        return new Vector3(x, y, z);
    }
}
