namespace BrokenGears.Combat {
    using UnityEngine;

    public abstract class AWeaponizedTurret : ATurret {
        [SerializeField] protected Transform target;
        [SerializeField] protected Transform defaultTarget;
        [SerializeField] protected Bone[] bones;

        protected virtual void Awake() {
            target = defaultTarget;
        }

        protected virtual void Update() {
            RotateParts();
        }

        protected void RotateParts() {
            if (target) {
                for (int i = 0; i < bones.Length; i++) {
                    bones[i].Rotate(target);
                }
            }
        }

        [System.Serializable]
        protected class Bone {
            [SerializeField] private Transform origin;
            [SerializeField] private Axis axis;
            [SerializeField] private bool useLocal;
            [SerializeField] private float turnSpeed;

            public void Rotate(Transform target) {
                Vector3 direction = (target.position - origin.position).normalized;
                Quaternion lookRotation = GetLookRotation(direction);

                Vector3 lerpedRotation = Quaternion.Lerp(origin.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
                Quaternion newRotation = GetNewPartRotation(lerpedRotation);
                ApplyPartRotation(newRotation);
            }

            private static Quaternion GetLookRotation(Vector3 direction) {
                if (direction != Vector3.zero) {
                    return Quaternion.LookRotation(direction);
                }

                return Quaternion.Euler(Vector3.zero);
            }

            private Quaternion GetNewPartRotation(Vector3 lerpedRotation) {
                Quaternion newRotation = Quaternion.identity;

                switch (axis) {
                    case Axis.x:
                        newRotation = Quaternion.Euler(lerpedRotation.x, 0f, 0f);
                        break;
                    case Axis.y:
                        newRotation = Quaternion.Euler(0f, lerpedRotation.y, 0f);
                        break;
                    case Axis.z:
                        newRotation = Quaternion.Euler(0f, 0f, lerpedRotation.z);
                        break;
                }

                return newRotation;
            }

            private void ApplyPartRotation(Quaternion newRotation) {
                if (useLocal) {
                    origin.localRotation = newRotation;
                    return;
                }

                origin.rotation = newRotation;
            }

            private enum Axis {
                x, y, z
            }
        }
    }
}
