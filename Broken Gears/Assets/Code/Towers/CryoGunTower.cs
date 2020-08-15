using UnityEngine;

public class CryoGunTower : GunTowerBase {

    [Tooltip("New speed for hit enemies by percentage")]
    public float freezeStrength;
    public float freezeDuration, fovAngle;

    [HideInInspector] public bool isSpraying;

    public override void AttackBehaviour() {
        if (isActive) {
            isHitting = false;
            base.AttackBehaviour();
            if (currentTarget) {
                if (!isSpraying) {
                    Tools.tools.StartStopParticleSystemsFromArray(attackParticles, true);
                    isSpraying = true;
                }
                if (isHitting) {
                    FreezeFX fx = rcHit.transform.GetComponentInParent<FreezeFX>();
                    if (!fx) {
                        fx = rcHit.transform.gameObject.GetComponentInParent<Enemy>().gameObject.AddComponent<FreezeFX>();
                        fx.freezeStrength = freezeStrength;
                        fx.durationInSeconds = freezeDuration;
                        fx.enemyAffected = currentTarget;
                        fx.shouldFX = true;
                    } else {
                        fx.durationSpendInSeconds = 0;
                    }                
                }
            } else if (isSpraying) {
                Tools.tools.StartStopParticleSystemsFromArray(attackParticles, false);
                isSpraying = false;
            }
        }
    }
}
