using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenGears.Old {
    public class WeaponizedTower : Tower {
        [Space, SerializeField] int enemyInRangeCheckPerSecond = 5;
        [SerializeField] protected float range, damagePerAttack, attackDelay;
        [SerializeField] protected Transform checkEnemiesFromHere, attackOrigin;
        [Space, SerializeField] protected WeaponFollow[] weaponParts;
        [Space, SerializeField] protected ParticleSystem[] attackParticles;

        protected bool isHitting;
        protected Enemy currentTarget;
        protected LayerMask ignoreLayers;

        private float attackTimer;
        private WaveSpawner spawner;
        private List<Enemy> enemiesInRange = new List<Enemy>();

        #region Get/Set
        public Enemy GetCurrentTarget() {
            return currentTarget;
        }
        #endregion

        protected override void Start() {
            base.Start();
            ignoreLayers = TowerManager.singleTM.GetIgnoreLayers();
            spawner = WaveSpawner.singleWS;
            for (int i = 0; i < weaponParts.Length; i++) {
                weaponParts[i].tower = this;
            }
            InvokeRepeating(nameof(CheckForEnemiesInRange), 0, 1f / enemyInRangeCheckPerSecond);
        }

        private void Update() {
            for (int i = 0; i < weaponParts.Length; i++) {
                weaponParts[i].RotateParts();
            }
            AttackBehaviour();
        }

        public virtual void AttackBehaviour() {
            if (currentTarget && isActive) {
                if (attackTimer > attackDelay) {
                    attackTimer = 0;
                    Attack();
                } else {
                    attackTimer += Time.deltaTime;
                }
            }
        }

        void CheckForEnemiesInRange() {
            for (int i = 0; i < spawner.enemiesOnTheField.Count; i++) {
                Enemy enemy = spawner.enemiesOnTheField[i];
                if (InRangeCheck(enemy) && !enemy.GetIsDead() && tManager.GetSelectedTower() != this) {
                    if (!enemiesInRange.Contains(enemy)) {
                        enemiesInRange.Add(enemy);
                        if (!currentTarget) {
                            SetNextTarget();
                        }
                    }
                } else {
                    if (enemiesInRange.Contains(enemy)) {
                        enemiesInRange.Remove(enemy);
                        if (currentTarget == enemy) {
                            SetNextTarget();
                        }
                    }
                }
            }

            if (currentTarget && (!InRangeCheck(currentTarget) || currentTarget.GetIsDead())) {
                SetNextTarget();
            }
        }

        void SetNextTarget() {
            currentTarget = null;
            for (int i = 0; i < enemiesInRange.Count; i++) {
                Enemy enemy = enemiesInRange[i];
                if (InRangeCheck(enemy) && !enemy.GetIsDead()) {
                    SetTarget(enemy);
                    break;
                }
            }
        }

        public bool InRangeCheck(Enemy enemy) {
            bool inRange = false;
            if (enemy) {
                Transform target = Tools.GetTarget(enemy);
                if (Vector3.Distance(target.position, checkEnemiesFromHere.position) < range) {
                    inRange = true;
                }
            }
            return inRange;
        }

        void SetTarget(Enemy enemy) {
            currentTarget = enemy;
        }

        public virtual void DoDamage(Enemy enemy) {
            if (enemy) {
                enemy.DoDamage(damagePerAttack);
            }
        }

        public virtual void Attack() { }

        public virtual void OnDrawGizmosSelected() {
            if (checkEnemiesFromHere) {
                Gizmos.DrawWireSphere(checkEnemiesFromHere.position, range);
            }
            for (int i = 0; i < weaponParts.Length; i++) {
                if (weaponParts[i].towerPart) {
                    Debug.DrawRay(weaponParts[i].towerPart.position, weaponParts[i].towerPart.forward, Color.red);
                }
            }
        }
    }

    [Serializable]
    public class WeaponFollow {
        public enum Axis {
            x,
            y,
            z
        }
        public Axis axis;
        public float turnSpeed = 3f;
        public bool useLocal;
        public Transform towerPart, defaultTarget;
        [HideInInspector] public WeaponizedTower tower;

        public void RotateParts() {
            Transform tp;
            Enemy current = tower.GetCurrentTarget();
            if (current != null) {
                tp = Tools.GetTarget(current);
            } else {
                tp = defaultTarget;
            }
            Vector3 dir = tp.position - towerPart.position;
            Quaternion lookRotation = Quaternion.Euler(Vector3.zero);
            if (dir != Vector3.zero) {
                lookRotation = Quaternion.LookRotation(dir);
            }
            Vector3 newFullRot = Quaternion.Lerp(towerPart.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            Quaternion newPartRotation = Quaternion.identity;
            switch (axis) {
                case Axis.x:
                newPartRotation = Quaternion.Euler(newFullRot.x, 0f, 0f);
                break;
                case Axis.y:
                newPartRotation = Quaternion.Euler(0f, newFullRot.y, 0f);
                break;
                case Axis.z:
                newPartRotation = Quaternion.Euler(0f, 0f, newFullRot.z);
                break;
            }
            if (useLocal) {
                towerPart.localRotation = newPartRotation;
            } else {
                towerPart.rotation = newPartRotation;
            }
        }
    }
}