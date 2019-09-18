using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiLookAt : MonoBehaviour {

    public static Transform transformer;

    private void Awake() {
        transformer = GetComponent<Transform>();
    }

}
