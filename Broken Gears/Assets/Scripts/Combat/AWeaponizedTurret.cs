namespace BrokenGears.Combat {
    using Enemies;
    using UnityEngine;
    using System.Collections.Generic;

    public abstract class AWeaponizedTurret : ATurret {
        [SerializeField, ReadOnly] protected AEnemy target;
        [SerializeField, ReadOnly] private List<AEnemy> enemiesInRange = new List<AEnemy>();
        [Space]
        [SerializeField] protected AEnemy defaultTarget;
        [SerializeField] protected float range;
        [SerializeField] protected Vector3 rangeOrigin;
        [SerializeField] protected Bone[] bones;

        protected virtual void Update() {
            RotateParts();
            CheckTargets();
        }

        protected void CheckTargets() {
            if(!TryGetOverlappingEnemies(out List<AEnemy> enemies)) {
                target = default;
                return;
            }

            for (int i = 0; i < enemiesInRange.Count; i++) {
                AEnemy enemy = enemiesInRange[i];

                if (!enemies.Contains(enemy)) {
                    enemiesInRange.Remove(enemy);
                }
            }

            for (int i = 0; i < enemies.Count; i++) {
                AEnemy enemy = enemies[i];
                
                if (!enemiesInRange.Contains(enemy)) {
                    enemiesInRange.Add(enemy);
                }
            }

            if(enemiesInRange.Count > 0) {
                target = enemiesInRange[0];
                return;
            }

            target = defaultTarget;
        }

        private bool TryGetOverlappingEnemies(out List<AEnemy> enemies) {
            enemies = new List<AEnemy>();

            if (!EnemyManager.Instance) {
                return false;
            }

            Collider[] colliders = Physics.OverlapSphere(transform.position + rangeOrigin, range, EnemyManager.Instance.Enemylayer);

            for (int i = 0; i < colliders.Length; i++) {
                AEnemy enemy = colliders[i].GetComponentInParent<AEnemy>();
                if (enemy && !enemies.Contains(enemy)) {
                    enemies.Add(enemy);
                }
            }

            return true;
        }

        protected void RotateParts() {
            if (target) {
                for (int i = 0; i < bones.Length; i++) {
                    bones[i].Rotate(target.transform);
                }
            }
        }

        private void OnDrawGizmosSelected() {
            Gizmos.DrawWireSphere(transform.position + rangeOrigin, range);
        }

        [System.Serializable]
        protected class Bone {
            [SerializeField] private Transform origin;
            [SerializeField] private Axis axis;
            [SerializeField] private bool useLocal;
            [SerializeField] private float turnSpeed;

            public Transform Origin => origin;
            public Axis Axis => axis;
            public bool UseLocal => useLocal;
            public float TurnSpeed => turnSpeed;

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
        }

        protected enum Axis {
            x, y, z
        }
    }
}
