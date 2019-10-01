using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public LayerMask layerMask;

    public Transform armTarget;
    public Transform weaponTarget;
    public Transform defaultArmTarget;
    public Transform defaultWeaponTarget;
    
    public Mesh mesh;
    public Material mat;

    public GameObject AttackSound;
    public GameObject pointOfAttack;
    public float turnSpeed;
    public float attackSpeed;
    public int dmg;
    public GameObject tempBullet;
    float attackDelay;
    public float range;
    public float rangetest;

    public GameObject weaponBase;

    public GameObject enemyCheck;

    public List<GameObject> targetsInRange = new List<GameObject>();

    private void Start() {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
    }

    private void Update() {
        enemyCheck.transform.LookAt(armTarget);
        Debug.DrawRay(enemyCheck.transform.position, enemyCheck.transform.forward, Color.red * 1000);
        if (armTarget != null && armTarget != defaultArmTarget) {
            if (attackDelay > attackSpeed) {
                Attack();
                attackDelay = 0;
            }
            attackDelay += Time.deltaTime;
        }
    }

    void Attack() {
        armTarget.GetComponentInParent<Health>().Damage(dmg);
        if (AttackSound != null && pointOfAttack != null && tempBullet != null) {
            Instantiate(AttackSound, pointOfAttack.transform.position, Quaternion.identity);
            RaycastHit hit;
            if (Physics.Raycast(pointOfAttack.transform.position, pointOfAttack.transform.forward, out hit, range/2)) {
                Instantiate(tempBullet, hit.point, Quaternion.identity);
            }
        }
    }

    void UpdateTarget() {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(enemyCheck.transform.position, enemyCheck.transform.forward, out hit, range)) {
            //print(hit.transform.name);
        }

        GameObject[] targets = GameObject.FindGameObjectsWithTag("EnemyTarget");

        for (int i = 0; i < targets.Length; i++) {
            if (distance(transform.position, targets[i].transform.position) < range && !targetsInRange.Contains(targets[i])) {
                targetsInRange.Add(targets[i]);
            } else if (targetsInRange.Contains(targets[i]) && distance(transform.position, targets[i].transform.position) > range) {
                targetsInRange.Remove(targets[i]);
            }
        }

        if (targetsInRange.Count > 0) {
            if (targetsInRange[0] != null) {
                armTarget = targetsInRange[0].transform;
                weaponTarget = targetsInRange[0].transform;
            } else {
                targetsInRange.RemoveAt(0);
            }
        } else {
            armTarget = defaultArmTarget;
            weaponTarget = defaultWeaponTarget;
        }
    }

    float distance(Vector3 vA, Vector3 vB) {
        return Vector3.Distance(vA, vB);
    }

    private void OnDrawGizmos() {
        Gizmos.color = mat.color;
        Vector3 rang = new Vector3(weaponBase.transform.position.x, transform.position.y, weaponBase.transform.position.z);
        Gizmos.DrawMesh(mesh, transform.position, Quaternion.identity, new Vector3(rangetest, 0f, rangetest));
    }
}
