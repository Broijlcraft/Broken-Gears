using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public Transform targetingPoint;
    public float maxHealth, destroyAfter;
    public Range scrapDroppedOnDeathBetween;
    [Header("HideInInspector")]
    public float currentHealth;

    [HideInInspector] public bool isDead;
    Animator anim;
    Collider[] colliders;

    private void Awake() {
        anim = GetComponentInChildren<Animator>();
        colliders = GetComponentsInChildren<Collider>();
        currentHealth = maxHealth;
    }

    public void DoDamage(float amount) {
        if(!isDead) {
            currentHealth -= amount;
            if(currentHealth <= 0) {
                Death();
            }
        }
    }

    public void Death() {
        for (int i = 0; i < colliders.Length; i++) {
            colliders[i].enabled = false;
        }
        isDead = true;
        currentHealth = 0;
        anim.SetBool("Death", true);
        Destroy(gameObject, destroyAfter);
    }
}
