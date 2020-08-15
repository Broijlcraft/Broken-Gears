using UnityEngine;

public class SawTower : WeaponizedTower {
    public float sawSize, maxSpeed, divider;
    public bool spin;
    float actualSpeed;

    public override void AttackBehaviour() {
        if (isHitting) {
            base.AttackBehaviour();
        }
    }

    public override void Attack() {
        DoDamage(currentTarget);
    }

    private void FixedUpdate() {
        if (currentTarget || spin) {
            if (actualSpeed < maxSpeed) {
                actualSpeed += maxSpeed / divider;
            } else {
                actualSpeed = maxSpeed;
            }
        } else {
            if (actualSpeed > 0) {
                actualSpeed -= maxSpeed / divider;
            } else {
                actualSpeed = 0;
            }
        }
        attackOrigin.Rotate(Vector3.forward * actualSpeed);
    }

    public void OnTriggerEnterAndExit(bool trigger) {
        if (isHitting != trigger && isActive) {
            isHitting = trigger;
            Tools.tools.StartStopParticleSystemsFromArray(attackParticles, isHitting);
        }
    }
}