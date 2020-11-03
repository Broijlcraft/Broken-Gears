public class FreezeFX : StatusFX {

    private void Update() {
        if (shouldFX) {
            enemyAffected.pathing.speed = freezeStrength / 100 * enemyAffected.pathing.defaultSpeed;
            Timer();
        }
    }

    public override void StopUsing() {
        shouldFX = false;
        enemyAffected.pathing.speed = enemyAffected.pathing.defaultSpeed;
        base.StopUsing();
    }
}