namespace BrokenGears {
    using UnityEngine;

    public class Tile : MonoBehaviour {
        [SerializeField] private bool canNotBeDestroyedOnGridClear;

        public bool CanNotBeDestroyedOnGridClear => canNotBeDestroyedOnGridClear;
    }
}