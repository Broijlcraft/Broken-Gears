public class FreezeFX : StatusFX {

    private void Update() {
        if (shouldFX) {
            enemyAffected.pathfinding.speed = freezeStrength / 100 * enemyAffected.pathfinding.defaultSpeed;
            Timer();
        }
    }

    public override void StopUsing() {
        shouldFX = false;
        enemyAffected.pathfinding.speed = enemyAffected.pathfinding.defaultSpeed;
        base.StopUsing();
    }
}