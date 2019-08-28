using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGrid : MonoBehaviour {

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public float cellSize = 1;
    public Vector3 gridOFFset;
    public int sizeX;
    public int sizeY;

    void Start() {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    void Update() {
        MakeContinuousProceduralGrid();
        UpdateMesh();
    }

    void MakeDiscreteProceduralGrid() {
        vertices = new Vector3[sizeX * sizeY * 4];
        triangles = new int[sizeX * sizeY * 6];

        int v = 0;
        int t = 0;

        float vertexOffset = cellSize * 0.5f;

        for (int x = 0; x < sizeX; x++) {
            for (int y = 0; y < sizeY; y++) {

                Vector3 cellOffset = new Vector3(x * cellSize, 0, y * cellSize);

                vertices[v] = new Vector3(-vertexOffset, 0, -vertexOffset) + cellOffset + gridOFFset;
                vertices[v + 1] = new Vector3(-vertexOffset, 0, vertexOffset) + cellOffset + gridOFFset;
                vertices[v + 2] = new Vector3(vertexOffset, 0, -vertexOffset) + cellOffset + gridOFFset;
                vertices[v + 3] = new Vector3(vertexOffset, 0, vertexOffset) + cellOffset + gridOFFset;

                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + 2;
                triangles[t + 5] = v + 3;

                v += 4;
                t += 6;
            }
        }
    }

    void MakeContinuousProceduralGrid() {
        vertices = new Vector3[(sizeX + 1) * (sizeY + 1)];
        triangles = new int[sizeX * sizeY * 6];

        int v = 0;
        int t = 0;

        float vertexOffset = cellSize * 0.5f;

        for (int x = 0; x <= sizeX; x++) {
            for (int y = 0; y <= sizeY; y++) {
                vertices[v] = new Vector3((x * cellSize) - vertexOffset, 0, (y * cellSize) - vertexOffset);
                v++;
            }
        }

        v = 0;

        for (int x = 0; x < sizeX; x++) {
            for (int y = 0; y < sizeY; y++) {
                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + (sizeY + 1);
                triangles[t + 5] = v + (sizeY + 1) + 1;
                v++;
                t += 6;
            }
            v++;
        }
    }

    void UpdateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
