using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public bool buildable;
    public Transform buildableParent;

    public void PlaceTower(GameObject tower) {
        print(tower.name);
    }
}
