using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static Transform canvas;
    public static Transform mobileCanvas;

    private void Awake() {
        canvas = GameObject.Find("Canvas").transform;
        mobileCanvas = GameObject.Find("MobileCanvas").transform;
    }
}
