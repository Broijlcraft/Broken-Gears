using UnityEngine;

namespace BrokenGears.Old {
    public class Tile : MonoBehaviour {

        [SerializeField] private Tile buildableParent;

        private bool buildable;
        private Tile buildableChild;
        private Vector3 targetPosition, targetRotation;

        private TowerManager tManager;

        #region Get/Set
        public bool GetIsBuildable() {
            return buildable;
        }

        public Vector3 GetTargetRotation() {
            return targetRotation;
        }

        public Vector3 GetTargetPosition() {
            return targetPosition;
        }

        public void SetBuildable(bool state) {
            buildable = state;
        }

        public Tile GetBuildableChild() {
            return buildableChild;
        }

        public void SetBuildableChild(Tile child) {
            buildableChild = child;
        }

        public Tile GetBuildableParent() {
            return buildableParent;
        }
        #endregion

        private void Awake() {
            if (buildableParent) {
                targetPosition = buildableParent.transform.position;
                buildable = true;
                buildableChild = this;
                buildableParent.SetBuildableChild(this);
                buildableParent.SetBuildable(true);
            } else {
                targetPosition = transform.position;
                buildableParent = this;
            }
        }

        private void Start() {
            tManager = TowerManager.singleTM;
            if (!tManager) { return; }
            if (buildableParent.transform.position.x == buildableChild.transform.position.x) {
                if (buildableParent.transform.position.z > buildableChild.transform.position.z) {
                    SetParentRotation(tManager.GetTowerRotations().minZRotation);
                } else {
                    SetParentRotation(tManager.GetTowerRotations().plusZRotation);
                }
            } else if (buildableParent.transform.position.z == buildableChild.transform.position.z) {
                if (buildableParent.transform.position.x > buildableChild.transform.position.x) {
                    SetParentRotation(tManager.GetTowerRotations().plusXRotation);
                } else {
                    SetParentRotation(tManager.GetTowerRotations().minXRotation);
                }
            }
        }

        void SetParentRotation(Vector3 rot) {
            buildableParent.targetRotation = rot;
        }
    }
}