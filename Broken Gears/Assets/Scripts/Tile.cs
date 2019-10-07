using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public bool buildable;
    public Transform buildableParent;
    [HideInInspector] public Transform child;
    public Vector3 setRotation;

    private void Start() {
        if (buildableParent != null) {
            buildableParent.GetComponent<Tile>().child = transform;
        }
    }
}
