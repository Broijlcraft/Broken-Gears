using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    [Header("Weapon Specifics")]

    public GameObject turretImg;
    public ParticleSystem weaponParticle;
    public ParticleSystem impactParticle;
    public GameObject AttackSound;
    public GameObject pointOfImpact;
    public float turnSpeed;
    public float turnSpeedSave;
    public float attackSpeed;
    public int dmg;
    public GameObject tempBullet;
    float attackDelay;
    public float range;
    float rangeSave;
    public float rangetest;

    public Transform weaponBase;

    [Header("Targeting")]

    public bool selected;
    public Transform armTarget;
    public Transform weaponTarget;
    public Transform defaultArmTarget;
    public Transform defaultWeaponTarget;
    public Transform enemyCheck;
    [HideInInspector]public List<GameObject> targetsInRange = new List<GameObject>();

    [Header("Gizmos")]

    public Mesh mesh;
    public Material mat;

    [Header("SpecialAttack")]

    public Animator animator;
    public float extraDelay;
    public string animationName;
    public float rotationDelay;
    public int rangeDividerRotation;

    private void Start() {
        turnSpeedSave = turnSpeed;
        animator = GetComponentInChildren<Animator>();
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
    }

    private void Update() {
        if (TowerManager.selectedTower != gameObject) {
            selected = false;
            enemyCheck.LookAt(armTarget);
            Debug.DrawRay(enemyCheck.position, enemyCheck.forward, Color.red * 1000);
            if (attackDelay > attackSpeed && armTarget != null && armTarget != defaultArmTarget) {
                Attack();
                attackDelay = 0;
            } else if (attackDelay >= 0 ) {
                attackDelay += Time.deltaTime;
            }        
        } else {
            selected = true;
        }
    }

    void Attack() {
        rangeSave = range;
        if (animator == null) {
            DoAttack();
        } else {
            range += range/rangeDividerRotation;
            InvokeRepeating("AnimAsian", extraDelay, 0f);
            InvokeRepeating("DoAttack", extraDelay + 0.2f, 0f);
            InvokeRepeating("ResetRotationSpeed", rotationDelay, 0);
        }
    }

    void ResetRotationSpeed() {
        turnSpeed = turnSpeedSave;
    }

    void DoAttack() {
        if (armTarget != null && armTarget != defaultArmTarget) {
            armTarget.GetComponentInParent<Health>().Damage(dmg);
            if (AttackSound != null && pointOfImpact != null && tempBullet != null) {
                Instantiate(AttackSound, pointOfImpact.transform.position, Quaternion.identity);
                RaycastHit hit;
                if (Physics.Raycast(pointOfImpact.transform.position, pointOfImpact.transform.forward, out hit, range/2)) {
                    Instantiate(tempBullet, hit.point, Quaternion.identity);
                }
            }
        }
        range = rangeSave;
    }

    void AnimAsian() {
        turnSpeed = 0;
        animator.SetTrigger(animationName);
    }

    void UpdateTarget() {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(enemyCheck.position, enemyCheck.forward, out hit, range)) {
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
        Vector3 rang = new Vector3(weaponBase.position.x, transform.position.y, weaponBase.position.z);
        Gizmos.DrawMesh(mesh, transform.position, Quaternion.identity, new Vector3(rangetest, 0f, rangetest));
    }
}
