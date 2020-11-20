using UnityEngine;

public class CryoGunTower : GunTowerBase {

    [Tooltip("New speed for hit enemies by percentage")]
    [SerializeField] private float freezeStrength;
    [SerializeField] private float freezeDuration, fovAngle;

    private bool isSpraying;

    public override void AttackBehaviour() {
        if (isActive) {
            isHitting = false;
            base.AttackBehaviour();
            if (currentTarget) {
                if (!isSpraying) {
                    Tools.StartStopParticleSystemsFromArray(attackParticles, true);
                    isSpraying = true;
                }
                if (isHitting) {
                    FreezeFX fx = rcHit.transform.GetComponentInParent<FreezeFX>();
                    if (!fx) {
                        fx = rcHit.transform.gameObject.GetComponentInParent<Enemy>().gameObject.AddComponent<FreezeFX>();
                        fx.SetAffected(currentTarget, freezeStrength, freezeDuration);
                    } else {
                        fx.ResetTimer();
                    }                
                }
            } else if (isSpraying) {
                Tools.StartStopParticleSystemsFromArray(attackParticles, false);
                isSpraying = false;
            }
        }
    }
}
