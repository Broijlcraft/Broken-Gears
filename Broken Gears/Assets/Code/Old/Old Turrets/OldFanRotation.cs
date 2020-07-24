using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldFanRotation : MonoBehaviour {

    public Vector3 rot;
    public float speed;

    private void Update() {
        transform.Rotate(rot * speed * Time.deltaTime);
    }
}
