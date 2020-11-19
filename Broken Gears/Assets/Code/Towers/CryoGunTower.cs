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
                    Tools.StartStopParticleSystemsFromArray(attackParticles, true);
                    isSpraying = true;
                }
                if (isHitting) {
                    FreezeFX fx = rcHit.transform.GetComponentInParent<FreezeFX>();
                    if (!fx) {
                        fx = rcHit.transform.gameObject.GetComponentInParent<Enemy>().gameObject.AddComponent<FreezeFX>();
                        fx.freezeStrength = freezeStrength;
                        fx.durationInSeconds = freezeDuration;
                        fx.SetAffected(currentTarget);
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
