using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponizedTower : Tower {
    [Space]
    public float range, damagePerAttack, attackDelay;
    public int enemyInRangeCheckPerSecond = 5;
    public Transform checkEnemiesFromHere, attackOrigin;
    [Space]
    public WeaponFollow[] weaponParts;
    [Space]
    public ParticleSystem[] attackParticles;

    [HideInInspector] public bool isHitting;
    [HideInInspector] public float attackTimer;
    [HideInInspector] public Enemy currentTarget;
    [HideInInspector] public List<Enemy> enemiesInRange = new List<Enemy>();

    private void Start() {
        for (int i = 0; i < weaponParts.Length; i++) {
            weaponParts[i].tower = this;
        }
        InvokeRepeating("CheckForEnemiesInRange", 0, 1f / enemyInRangeCheckPerSecond);
    }

    private void Update() {
        if (isActive) {
            for (int i = 0; i < weaponParts.Length; i++) {
                weaponParts[i].RotateParts();
            }
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
        if (isActive) {
            for (int i = 0; i < WaveSpawner.ws_Single.enemiesOnTheField.Count; i++) {
                Enemy enemy = WaveSpawner.ws_Single.enemiesOnTheField[i];
                if (InRangeCheck(enemy) && !enemy.isDead) {
                    if (!enemiesInRange.Contains(enemy)) {
                        enemiesInRange.Add(enemy);
                        if (!currentTarget) {
                            SetNextTarget();
                        }
                    }
                } else {
                    if (enemiesInRange.Contains(enemy)) {
                        enemiesInRange.Remove(enemy);
                        if(currentTarget == enemy) {
                            SetNextTarget();
                        }
                    }
                }
            }
        }
    }    

    void SetNextTarget() {
        currentTarget = null;
        for (int i = 0; i < enemiesInRange.Count; i++) {
            Enemy enemy = enemiesInRange[i];
            if (InRangeCheck(enemy)) {
                SetTarget(enemy);
                break;
            }
        }
    }

    public bool InRangeCheck(Enemy enemy) {
        bool inRange = false;
        if (enemy) {
            Transform target = Tools.tools.GetTarget(enemy);
            if(Vector3.Distance(target.position, checkEnemiesFromHere.position) < range) {
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
        if(checkEnemiesFromHere) {
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

    public void RotateParts () {
        Transform tp;
        if (tower.currentTarget != null) {
            tp = Tools.tools.GetTarget(tower.currentTarget);
        } else {
            tp = defaultTarget;
        }
        Vector3 dir = tp.position - towerPart.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
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