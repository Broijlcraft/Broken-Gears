using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusFX : MonoBehaviour {
    public float durationInSeconds, freezeStrength;

    [HideInInspector] public float durationSpendInSeconds;
    [HideInInspector] public Enemy enemyAffected;
    [HideInInspector] public bool shouldFX;

    public void Timer() {
        if (durationSpendInSeconds < durationInSeconds) {
            durationSpendInSeconds += Time.deltaTime;
        } else {
            StopUsing();
        }
    }

    public virtual void StopUsing() {
        Destroy(this);
    }
}
