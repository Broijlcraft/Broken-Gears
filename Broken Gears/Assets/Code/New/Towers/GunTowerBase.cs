using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTowerBase : Tower {
    [Space]
    public Transform barrel;
    [HideInInspector] public RaycastHit rcHit;
    [HideInInspector] public bool isHitting;

    public override void AttackBehaviour() {
        isHitting = false;
        if (currentTarget) {
            if(attackTimer > attackDelay) {
                attackTimer = 0;
                Attack();
            } else {
                attackTimer += Time.deltaTime;
            }
        }
    }

    public override void Attack() {
        if(Physics.Raycast(barrel.position, barrel.forward, out rcHit, range, ~TowerManager.tm_Single.layersToIgnore)) {
            if (rcHit.transform.CompareTag("Enemy")) {
                isHitting = true;
                DoDamage(rcHit.transform.GetComponentInParent<Enemy>());
            }
        }
    }

    public override void OnDrawGizmos() {
        base.OnDrawGizmos();
        if (barrel) {
            Debug.DrawRay(barrel.position, barrel.forward);
        }
    }
}