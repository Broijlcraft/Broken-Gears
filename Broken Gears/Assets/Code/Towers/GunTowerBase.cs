using UnityEngine;

public class GunTowerBase : WeaponizedTower {
    protected RaycastHit rcHit;
    
    public override void Attack() {
        if(Physics.Raycast(attackOrigin.position, attackOrigin.forward, out rcHit, range, ~ignoreLayers)) {
            if (rcHit.transform.CompareTag("Enemy")) {
                isHitting = true;
                DoDamage(rcHit.transform.GetComponentInParent<Enemy>());
            }
        }
    }

    public override void OnDrawGizmosSelected() {
        base.OnDrawGizmosSelected();
        if (attackOrigin) {
            Debug.DrawRay(attackOrigin.position, attackOrigin.forward);
        }
    }
}