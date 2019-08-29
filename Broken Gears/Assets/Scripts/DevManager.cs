using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevManager : MonoBehaviour {

    Transform devCam;
    Transform mainCam;
    Vector3 leftOffPos;

    private void Start() {
        devCam = GameObject.Find("DevCam").transform;
        mainCam = GameObject.Find("Main Camera").transform;
        devCam.gameObject.SetActive(false);
        devCam.position = mainCam.position;
    }

    private void Update() {
        if (Input.GetButtonDown("NoClip")) {
            if (mainCam.gameObject.activeSelf == true) {
                mainCam.gameObject.SetActive(false);
                devCam.gameObject.SetActive(true);
            } else {
                mainCam.gameObject.SetActive(true);
                devCam.gameObject.SetActive(false);
            }
        }
    }
}
