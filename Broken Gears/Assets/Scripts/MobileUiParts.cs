using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MobileUiParts : MonoBehaviour {
    [HideInInspector] public Transform parent;
    public Vector3 offSet;
    public bool stay;
    public float destroyDelay;

    private void Update() {
        transform.LookAt(Camera.main.transform);
        if (stay == true) {
            if (parent != null) {
                transform.position = parent.position + offSet;
            }
        } else {
            Destroy(gameObject, destroyDelay);
        }
    }
}
