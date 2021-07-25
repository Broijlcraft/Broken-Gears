using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenGears {
    public class GridManager : MonoBehaviour {
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private Tile tilePrefab;
        [ReadOnly] public List<Tile> tiles = null;

        public Vector2Int GridSize => gridSize;
        public Tile TilePrefab => tilePrefab;
        
        private void Start() {

        }
        private void OnDrawGizmosSelected() {
            Vector3 size = new Vector3(gridSize.x, 0, gridSize.y);
            Gizmos.DrawWireCube(transform.position, size);
        }
    }
}