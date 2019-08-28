using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public int gridX;
    public int gridY;

    public bool blocked;
    public Vector3 position;

    public Node (bool blocking, Vector3 positionA, int gridX_a, int GridY_a) {
        blocked = blocking;
        position = positionA;
        gridX = gridX_a;
        gridY = GridY_a;
    }
}
