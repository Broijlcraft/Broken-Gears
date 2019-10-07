using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public bool buildable;
    public Transform buildableParent;
    public Transform child;
    public Vector3 setPosition;
    public Vector3 setRotation;

    private void Start() {
        if (buildableParent != null) {
            setPosition = buildableParent.transform.position;
        } else {
            setPosition = transform.position;
        }
    }
}
