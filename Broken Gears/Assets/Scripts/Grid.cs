using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    public Transform startPos;
    public LayerMask wallMasky;
    public Vector2 gridWolrdSize;
    public float nodeRadius;
    public float distance;

    Node[,] grid;
    public List<Node> finalPath;
    float nodeDiameter;
    int gridSizeX, gridSizeY;
    
    private void Start() {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWolrdSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWolrdSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid() {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWolrdSize.x / 2 - Vector3.forward * gridWolrdSize.y / 2;
        for (int y = 0; y < gridSizeY; y++) {
            for(int x = 0; x < gridSizeX; x++) {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool wall_E = true;
                if (Physics.CheckSphere(worldPoint, nodeRadius, wallMasky)) {
                    wall_E = false;
                }
                grid[y, x] = new Node(wall_E, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 a_WorldPosition) {
        float xpoint = ((a_WorldPosition.x = gridWolrdSize.x / 2) / gridWolrdSize.x);
        float ypoint = ((a_WorldPosition.z = gridWolrdSize.y / 2) / gridWolrdSize.y);

        xpoint = Mathf.Clamp01(xpoint);
        ypoint = Mathf.Clamp01(ypoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

        return grid[x, y];        
    }

    public List<Node> GetNeighboringNodes (Node a_Node) {
        List<Node> NeighboringNodes = new List<Node>();
        int xCheck;
        int yCheck;

        //right side
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY;

        if (xCheck >= 0 && xCheck < gridSizeX) {
            if (yCheck >= 0 && yCheck < gridSizeY) {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //left side
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY;

        if (xCheck >= 0 && xCheck < gridSizeX) {
            if (yCheck >= 0 && yCheck < gridSizeY) {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }


        //top check
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY + 1;

        if (xCheck >= 0 && xCheck < gridSizeX) {
            if (yCheck >= 0 && yCheck < gridSizeY) {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //bottom check
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY - 1;

        if (xCheck >= 0 && xCheck < gridSizeX) {
            if (yCheck >= 0 && yCheck < gridSizeY) {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        return NeighboringNodes;

    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - Vector3.right * gridWolrdSize.x / 2 - Vector3.forward * gridWolrdSize.y / 2, new Vector3(gridWolrdSize.x, 1, gridWolrdSize.y));
        if (grid != null) {
            foreach (Node n in grid) {
                if (n.IsWall) {
                    Gizmos.color = Color.white;
                } else {
                    Gizmos.color = Color.yellow;
                }

                if (finalPath != null) {
                    if (finalPath.Contains(n)) {
                        Gizmos.color = Color.red;
                    }
                }

                Gizmos.DrawCube(n.Position, Vector3.one * (nodeDiameter - distance));

            }
        }
    }
}
