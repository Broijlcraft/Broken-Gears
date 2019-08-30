using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCheck : MonoBehaviour {

    public Vector2 gridSize;
    public GameObject[] tile;
    Vector3 v;
    List<GameObject> gList = new List<GameObject>();

    private void Start() {
        CreateAGrid();
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            for(int i = 0; i < gList.Count; i++) {
                Destroy(gList[i]);
            }
            gList.Clear();
            CreateAGrid();
        }
    }

    void CreateAGrid() {
        v = transform.position;
        for (int i = 0; i < gridSize.x; i++) {
            for (int ib = 0; ib < gridSize.y; ib++) {
                int t = Random.Range(0, tile.Length);
                GameObject g = Instantiate(tile[t], v, Quaternion.identity);
                g.transform.SetParent(transform);
                gList.Add(g);
                v.x++;
            }
            v.x = transform.position.x;
            v.z++;
        }
    }
}
