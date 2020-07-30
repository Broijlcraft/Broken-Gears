using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTowerBase : Tower {
    [Space]
    public Transform barrel;

    public override void AttackBehaviour() {
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
        RaycastHit hit;
        if(Physics.Raycast(barrel.position, barrel.forward, out hit, range, ~TowerManager.tm_Single.layersToIgnore)) {
            if (hit.transform.CompareTag("Enemy")) {
                DoDamage(hit.transform.GetComponentInParent<Enemy>());
            }
        }
    }

    public override void OnDrawGizmos() {
        base.OnDrawGizmos();
        if (barrel) {
            Debug.DrawRay(barrel.position, barrel.forward * 1);
        }
    }
}