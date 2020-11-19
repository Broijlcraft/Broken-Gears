using UnityEngine;

public class StatusFX : MonoBehaviour {
    public float durationInSeconds, freezeStrength;

    protected float durationSpendInSeconds;
    protected Enemy enemyAffected;
    protected EnemyPathing enemyPathing;
    protected bool shouldFX;

    protected void Timer() {
        if (durationSpendInSeconds < durationInSeconds) {
            durationSpendInSeconds += Time.deltaTime;
        } else {
            StopUsing();
        }
    }

    public void ResetTimer() {
        durationSpendInSeconds = 0f;
    }

    public virtual void SetAffected(Enemy enemy) {
        enemyAffected = enemy;
        enemyPathing = enemy.GetEnemyPathing();
        shouldFX = true;
    }

    public virtual void StopUsing() {
        Destroy(this);
    }
}
