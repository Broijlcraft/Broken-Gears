using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {

    public GameObject g;

    private void Start() {
        Instantiate(g, transform.position, transform.rotation);
    }

}
