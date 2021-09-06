using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenGears {
    public class Test : MonoBehaviour {
        [SerializeField] private float tilesize = 1f;
        [SerializeField] private Vector3Int gridsize;

        [SerializeField] private Transform start;
        [SerializeField] private Transform end;


        private void OnDrawGizmos() {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridsize.x + .1f, 1.1f, gridsize.z + .1f));
            for (int x = 0; x < gridsize.x; x++) {
                for (int z = 0; z < gridsize.z; z++) {
                    float halfX = Mathf.RoundToInt(transform.position.x - (gridsize.x / 2));
                    float halfZ = Mathf.RoundToInt(transform.position.z - (gridsize.z / 2));

                    halfX += x;
                    halfZ += z;

                    halfX += gridsize.x % 2 == 0 ? tilesize / 2f : 0f;
                    halfZ += gridsize.z % 2 == 0 ? tilesize / 2f : 0f;

                    Gizmos.color = Color.gray;
                    Gizmos.DrawWireCube(new Vector3(halfX, 0, halfZ), Vector3.one * tilesize);
                }
            }
        }
    }
}