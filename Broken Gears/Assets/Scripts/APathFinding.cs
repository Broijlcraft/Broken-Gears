using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APathFinding : MonoBehaviour {
    public Grid aGrid;
    public Transform enemy;
    public Transform player;

    private void Awake() {
        //aGrid = GetComponent<Grid>();
    }

    private void Start() {
        
    }

    private void Update() {
        //FindPath(StartPosition.position, TargetPosition.position);
        if (Input.GetMouseButton(0)) {
            FindPath(enemy.position, player.position);
        }
    }

    void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos) {
        Node StartNode = aGrid.NodeFromWorldPosition(a_StartPos);
        Node TargetNode = aGrid.NodeFromWorldPosition(a_TargetPos);
        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(StartNode);
        print("Start " + StartNode);
        while (OpenList.Count > 0) {
            Node CurrentNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++) {
                if (OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].hCost < CurrentNode.hCost) {
                    CurrentNode = OpenList[i];
                }
            }

            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);

            if (CurrentNode == TargetNode) {
                GetFinalPath(StartNode, TargetNode);
            }

            foreach(Node NeighborNode in aGrid.GetNeighboringNodes(CurrentNode)) {
                if (!NeighborNode.IsWall || ClosedList.Contains(NeighborNode)) {
                    continue;
                }

                int MoveCost = CurrentNode.gCost + GetManHattenDistance(CurrentNode, NeighborNode);

                if (MoveCost < NeighborNode.gCost || !OpenList.Contains(NeighborNode)) {
                    NeighborNode.gCost = MoveCost;
                    NeighborNode.hCost = GetManHattenDistance(NeighborNode, TargetNode);
                    NeighborNode.Parent = CurrentNode;

                    if (!OpenList.Contains(NeighborNode)) {
                        OpenList.Add(NeighborNode);
                    }
                }
            }
        }
    }

    void GetFinalPath(Node a_StartingNode, Node a_EndNode) {
        List<Node> FinalPath = new List<Node>();
        Node CurrentNode = a_EndNode;

        while (CurrentNode != a_StartingNode) {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.Parent;
        }

        FinalPath.Reverse();

        aGrid.finalPath = FinalPath;
    }

    int GetManHattenDistance(Node a_nodeA, Node a_nodeB) {
        int ix = Mathf.Abs(a_nodeA.gridX - a_nodeB.gridX);
        int iy = Mathf.Abs(a_nodeA.gridY - a_nodeB.gridY);

        return ix + iy;
    }
}
