using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    public string towerName;
    public float range, damagePerAttack, attackDelay;
    public int enemyInRangeCheckPerSecond;
    public Transform checkEnemiesFromHere;

    public WeaponFollow[] weaponFollows;

    [Header("test")]
    public bool testEmmision;

    List<Material> mats = new List<Material>();
    [HideInInspector] public float attackTimer;
    /*[HideInInspector]*/ public Enemy currentTarget;
    [HideInInspector] public List<Enemy> enemiesInRange = new List<Enemy>();

    private void Start() {
        if (testEmmision) {
            MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < meshRenderers.Length; i++) {
                mats.Add(meshRenderers[i].material);
                mats[mats.Count-1].EnableKeyword("_EmissionColor");
                mats[mats.Count - 1].SetColor("_EmissionColor", Color.red);
            }
        }
        for (int i = 0; i < weaponFollows.Length; i++) {
            weaponFollows[i].tower = this;
        }
        InvokeRepeating("CheckForEnemiesInRange", 0, 1f / enemyInRangeCheckPerSecond);
    }

    private void Update() {
        for (int i = 0; i < weaponFollows.Length; i++) {
            weaponFollows[i].RotateParts();
        }
        AttackBehaviour();
    }

    public virtual void AttackBehaviour() { }

    public virtual void Attack() { }

    public virtual void DoDamage(Enemy enemy) {
        if (enemy) {
            enemy.DoDamage(damagePerAttack);
        }
    }

    void CheckForEnemiesInRange() {
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
        if (!InRangeCheck(currentTarget)) {
            SetNextTarget();
        }
    }    

    void SetNextTarget() {
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
            Transform target = GetTarget(enemy);
            if(Vector3.Distance(target.position, checkEnemiesFromHere.position) < range) {
                inRange = true;
            }
        }
        return inRange;
    }

    public Transform GetTarget(Enemy enemy) {
        Transform tp = enemy.transform;
        if (enemy.targetingPoint) {
            tp = enemy.targetingPoint;
        }
        return tp;
    }

    void SetTarget(Enemy enemy) {
        currentTarget = enemy;
    }

    public virtual void OnDrawGizmos() {
        if(checkEnemiesFromHere) {
            Gizmos.DrawWireSphere(checkEnemiesFromHere.position, range);
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
    public float turnSpeed;
    public bool useLocal;
    public Transform towerPart, defaultTarget;
    [HideInInspector] public Tower tower;

    public void RotateParts () {
        if (tower.currentTarget != null) {
            Transform tp = tower.GetTarget(tower.currentTarget);
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
}