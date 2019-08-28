using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyGrid : MonoBehaviour {

    public GameObject tile;
    public Vector2 mapSize;
    float gridsize;

    private void Start() {
        SetSize();
        Vector3 v = transform.position;
        for (int i =  0; i < mapSize.x; i++) {
            GameObject g = Instantiate(tile, v, Quaternion.identity);
            v.z += 2;
            g.transform.SetParent(transform);
        }
    }

    private void Update() {
        //SetSize();
    }

    void SetSize() {
        gridsize = mapSize.x * mapSize.y;
        print(gridsize);
    }
}
