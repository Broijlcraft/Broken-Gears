using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    public LayerMask wallMasky;
    public Vector2 gridWolrdSize;
    public float nodeRadius;
    public float distance;

    Node[,] grid;
    float nodeDiameter;
    int gridSizeX, gridSizeY;
    
    private void Start() {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWolrdSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWolrdSize.y / nodeDiameter);
        CreateGrid();
    }

    private void Update() {
        CreateGrid();
    }

    void CreateGrid() {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWolrdSize.x / 2 - Vector3.forward * gridWolrdSize.y / 2;
        for (int y = 0; y < gridSizeY; y++) {
            for (int x = 0; x < gridSizeX; x++) {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool wall_E = true;
                if (Physics.CheckSphere(worldPoint, nodeRadius, wallMasky)) {
                    wall_E = false;
                }
                grid[y, x] = new Node(wall_E, worldPoint, x, y);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWolrdSize.x, 1, gridWolrdSize.y));
        if (grid != null) {
            foreach (Node n in grid) {
                if (n.blocked) {
                    Gizmos.color = Color.white;
                } else {
                    Gizmos.color = Color.yellow;
                }

                Gizmos.DrawCube(n.position, Vector3.one * (nodeDiameter - distance));
            }
        }
    }
}
