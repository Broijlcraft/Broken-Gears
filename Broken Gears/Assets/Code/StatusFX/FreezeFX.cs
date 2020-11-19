public class FreezeFX : StatusFX {

    private void Update() {
        if (shouldFX) {
            enemyPathing.speed = freezeStrength / 100 * enemyPathing.defaultSpeed;
            Timer();
        }
    }

    public override void StopUsing() {
        shouldFX = false;
        enemyPathing.speed = enemyPathing.defaultSpeed;
        base.StopUsing();
    }
}