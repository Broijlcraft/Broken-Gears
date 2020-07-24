using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldTurret : MonoBehaviour {

    [Header("Weapon Specifics")]

    public string turretName;
    public ParticleSystem[] weaponParticle;
    public GameObject turretImg, impactParticle, attackSound, pointOfAttack;
    public float turnSpeed, turnSpeedSave, attackSpeed;
    public int dmg;
    float attackDelay, rangeSave;
    public float range, rangetest;
    public GameObject coll;
    public Transform weaponBase;
    [HideInInspector] public bool sawCollision;

    [Header("Parts For Emission")]

    public List<Renderer> weaponParts = new List<Renderer>();

    [Header("Targeting")]

    public bool selected;
    public Transform armTarget, weaponTarget, defaultArmTarget, defaultWeaponTarget, enemyCheck;
    [HideInInspector] public List<GameObject> targetsInRange = new List<GameObject>();

    [Header("Gizmos")]

    public Mesh mesh;
    public Material mat;

    [Header("SpecialAttack")]

    public Animator animator;
    public float extraDelay;
    public string animationName;
    public float rotationDelay;
    public int rangeDividerRotation;
    public bool isSaw, isCryo, isRivet;
    public float cryoSlow;

    [Header("StompAttack")]

    public Transform enemyChecker;

    private void Start() {
        turnSpeedSave = turnSpeed;
        animator = GetComponentInChildren<Animator>();
        coll = transform.Find("TurretCollider").gameObject;
        if (OldTowerManager.old_tm_Single.selectedTower == gameObject) {
            coll.SetActive(false);
        }
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
    }

    private void Update() { 
        if (isCryo == true) {
            if (armTarget == null || armTarget == defaultArmTarget) {
                for (int i = 0; i < weaponParticle.Length; i++) {
                    weaponParticle[i].Stop();
                }
            }
        }
        if (OldTowerManager.old_tm_Single.selectedTower != gameObject) {
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
            armTarget.GetComponentInParent<OldHealth>().Damage(dmg);
            if (isCryo == true) {
                armTarget.GetComponentInParent<EnemyPathing>().speed = armTarget.GetComponentInParent<EnemyPathing>().defaultSpeed / cryoSlow;
                for (int i = 0; i < weaponParticle.Length; i++) {
                    if (weaponParticle[i].isPaused || weaponParticle[i].isStopped) {
                        weaponParticle[i].Play();
                    }
                }
            }
            if (isSaw == true) {
                if (attackSound != null) {
                    Instantiate(attackSound, pointOfAttack.transform.position, Quaternion.identity);
                }
            }
            if (isRivet == true) {
                Instantiate(weaponParticle[0].transform.gameObject, pointOfAttack.transform.position, pointOfAttack.transform.rotation);
            }
        }
        range = rangeSave;
    }

    void AnimAsian() {
        turnSpeed = 0;
        animator.SetTrigger(animationName);
    }

    void UpdateTarget() {
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
