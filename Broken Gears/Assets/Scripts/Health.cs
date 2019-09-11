using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;

    private void Start() {
        currentHealth = maxHealth;
    }

    public void Damage(int dmg) {
        currentHealth -= dmg;
        if (currentHealth <= 0) {
            currentHealth = 0;
            Death();
        }
    }

    public void Death() {

    }
}
