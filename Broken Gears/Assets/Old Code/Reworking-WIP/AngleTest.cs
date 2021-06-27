using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenGears.Old {
    public class AngleTest : MonoBehaviour {
        public LayerMask enemyLayer;
        public Transform sprayOrigin;
        public float sprayAngle, range;
        public Transform[] targetsInRange;
        public List<Material> affectedMaterials = new List<Material>();

        private void FixedUpdate() {
            if (affectedMaterials.Count > 0) {
                for (int i = 0; i < affectedMaterials.Count; i++) {
                    affectedMaterials[i].color = Color.white;
                }
            }
            affectedMaterials.Clear();
            targetsInRange = GetTargetsInRange();
            if (targetsInRange.Length > 0) {
                for (int i = 0; i < targetsInRange.Length; i++) {
                    //List <Vector3> vertexPositions = GetVertexPositions(enemiesInRange[i].transform, enemiesInRange[i].GetComponent<MeshFilter>().mesh);
                    //for (int iB = 0; iB < vertexPositions.Count; iB++) {
                    //    Vector3 direction = (vertexPositions[iB] - sprayOrigin.position);

                    //    float angle = Vector3.Angle(direction, sprayOrigin.forward);
                    //    if (angle < sprayAngle / 2) {
                    //        affectedMaterials.Add(enemiesInRange[i].GetComponent<MeshRenderer>().material);
                    //        affectedMaterials[affectedMaterials.Count - 1].color = Color.cyan;
                    //        break;
                    //    }
                    //}

                    Vector3 direction = (targetsInRange[i].position - sprayOrigin.position);

                    float angle = Vector3.Angle(direction, sprayOrigin.forward);
                    if (angle < sprayAngle) {
                        affectedMaterials.Add(targetsInRange[i].GetComponent<MeshRenderer>().material);
                        affectedMaterials[affectedMaterials.Count - 1].color = Color.cyan;
                    }
                }
            }
        }

        Transform[] GetTargetsInRange() {
            Collider[] collidersInRange = Physics.OverlapSphere(transform.position, range, enemyLayer);
            List<Transform> targetingPoints = new List<Transform>();
            if (collidersInRange.Length > 0) {
                for (int i = collidersInRange.Length - 1; i >= 0; i--) {
                    Enemy enemy = null;
                    try {
                        enemy = collidersInRange[i].GetComponent<Enemy>();
                    } catch {
                        if (collidersInRange[i].transform.parent != null) {
                            enemy = collidersInRange[i].GetComponentInParent<Enemy>();
                        }
                    }
                    if (enemy && !targetingPoints.Contains(Tools.GetTarget(enemy))) {
                        targetingPoints.Add(Tools.GetTarget(enemy));
                    }
                }
            }
            return targetingPoints.ToArray();
        }

        //List<Vector3> GetVertexPositions(Transform localTransform, Mesh mesh) {
        //    List<Vector3> vertexPositions = new List<Vector3>();
        //    Matrix4x4 localToWorld = localTransform.localToWorldMatrix;
        //    for (int i = 0; i < mesh.vertices.Length; ++i) {
        //        Vector3 world_v = localToWorld.MultiplyPoint3x4(mesh.vertices[i]);
        //        if (!vertexPositions.Contains(world_v)) {
        //            vertexPositions.Add(world_v);
        //        }
        //    }
        //    return vertexPositions;
        //}

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(transform.position, range);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(sprayOrigin.position, GetAngledRotation(-sprayAngle / 2, Vector3.up, sprayOrigin.forward) * range);
            Gizmos.DrawRay(sprayOrigin.position, GetAngledRotation(sprayAngle / 2, Vector3.up, sprayOrigin.forward) * range);
            Gizmos.DrawRay(sprayOrigin.position, GetAngledRotation(-sprayAngle / 2, Vector3.right, sprayOrigin.forward) * range);
            Gizmos.DrawRay(sprayOrigin.position, GetAngledRotation(sprayAngle / 2, Vector3.right, sprayOrigin.forward) * range);
        }

        Vector3 GetAngledRotation(float halfAngle, Vector3 firstDirection, Vector3 secondDirection) {
            Vector3 angledAxis = Quaternion.AngleAxis(halfAngle, firstDirection) * secondDirection;
            // print(angledAxis);
            return angledAxis;
        }
    }
}