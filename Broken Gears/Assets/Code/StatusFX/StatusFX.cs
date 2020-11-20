using UnityEngine;

public class StatusFX : MonoBehaviour {
    protected bool shouldFX;
    protected Enemy enemyAffected;
    protected EnemyPathing enemyPathing;
    protected float durationInSeconds, freezeStrength, durationSpendInSeconds;

    #region Get/Set
    public float GetStrenght() {
        return freezeStrength;
    }
    #endregion

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

    public virtual void SetAffected(Enemy enemy, float strength, float duration) {
        freezeStrength = strength;
        durationInSeconds = duration;
        enemyAffected = enemy;
        enemyPathing = enemy.GetEnemyPathing();
        shouldFX = true;
    }

    protected virtual void StopUsing() {
        Destroy(this);
    }
}
