using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    public string towerName;
    public float range, damagePerAttack, attackDelay;
    public int enemyInRangeCheckPerSecond;
    public Transform transform_checkEnemiesFromHere;

    public WeaponFollow[] weaponFollows;

    [HideInInspector] public Enemy currentTarget;
    [HideInInspector] public List<Enemy> enemiesInRange = new List<Enemy>();

    private void Start() {
        for (int i = 0; i < weaponFollows.Length; i++) {
            weaponFollows[i].tower = this;
        }
        InvokeRepeating("CheckForEnemiesInRange", 0, 1f / enemyInRangeCheckPerSecond);
    }

    private void FixedUpdate() {
        for (int i = 0; i < weaponFollows.Length; i++) {
            weaponFollows[i].RotateParts();
        }
    }

    void CheckForEnemiesInRange() {
        for (int i = 0; i < WaveSpawner.ws_Single.enemiesOnTheField.Count; i++) {
            Enemy enemy = WaveSpawner.ws_Single.enemiesOnTheField[i];
            if (InRangeCheck(enemy)) {
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

    public bool InRangeCheck(Enemy enemy) {
        bool inRange = false;
        if (enemy) {
            Transform target = GetTarget(enemy);
            if(Vector3.Distance(target.position, transform_checkEnemiesFromHere.position) < range) {
                inRange = true;
            }
        }
        return inRange;
    }

    void SetNextTarget() {
        for (int i = 0; i < WaveSpawner.ws_Single.enemiesOnTheField.Count; i++) {
            Enemy enemy = WaveSpawner.ws_Single.enemiesOnTheField[i];
            print(enemy);
            Transform targetPoint = GetTarget(enemy);
            if (range > Vector3.Distance(targetPoint.position, transform_checkEnemiesFromHere.position)) {
                SetTarget(enemy);
                break;
            }
        }
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

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform_checkEnemiesFromHere.position, range);
    }
}

[Serializable]
public class WeaponFollow {
    public bool test;
    public enum Axis {
        x,
        y,
        z
    }
    public Axis axis;
    public float turnSpeed;
    public Transform towerPart, defaultTarget;
    [HideInInspector] public Tower tower;

    public void RotateParts () {
        if (test && tower.currentTarget != null) {
            Transform tp = tower.GetTarget(tower.currentTarget);
            Vector3 armDir = tp.position - tower.transform_checkEnemiesFromHere.position;
            Quaternion armLookRotation = Quaternion.LookRotation(armDir);
            Vector3 armRotation = Quaternion.Lerp(towerPart.rotation, armLookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            switch (axis) {
                case Axis.x:
                    towerPart.localRotation = Quaternion.Euler(armRotation.x, 0f, 0f);
                break;
                case Axis.y:
                    towerPart.localRotation = Quaternion.Euler(0f, armRotation.y, 0f);
                    Debug.DrawRay(towerPart.position, towerPart.forward);
                break;
                case Axis.z:
                    towerPart.localRotation = Quaternion.Euler(0f, 0f, armRotation.z);
                    Debug.DrawRay(towerPart.position, -towerPart.right);
                break;
            }
        }
    }
}