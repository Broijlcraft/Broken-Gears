using UnityEngine;

public class GunTowerBase : WeaponizedTower {
    protected RaycastHit rcHit;
    [SerializeField] private GameObject impactPart;

    public override void Attack() {
        if(Physics.Raycast(attackOrigin.position, attackOrigin.forward, out rcHit, range, ~ignoreLayers)) {
            if (rcHit.transform.CompareTag("Enemy")) {
                isHitting = true;
                if (impactPart) {
                    Instantiate(impactPart, rcHit.point, Quaternion.FromToRotation(Vector3.up, rcHit.normal));
                }
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