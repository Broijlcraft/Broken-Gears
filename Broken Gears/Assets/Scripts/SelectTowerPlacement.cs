using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTowerPlacement : MonoBehaviour {

    public RaycastHit hit;

    private void Update() {
        //transform.localPosition = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray ray = Manager.cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000)) {
            transform.position = hit.point;
        }
    }
}
