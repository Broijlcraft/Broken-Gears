﻿using UnityEngine;

public class Enemy : MonoBehaviour {
    public RobotType robotType;
    public Transform attackTargetingPoint;
    public float maxHealth, destroyAfter, verticalHealthBarOffSet;
    public Range scrapDroppedOnDeathBetween;
    public float currentHealth;

    Animator anim;
    bool isActive;
    Collider[] colliders;
    MobileUiHealth mobileUiHealth;
    [HideInInspector] public bool isDead;
    [HideInInspector] public EnemyPathing pathing;

    private void Awake() {
        anim = GetComponentInChildren<Animator>();
        colliders = GetComponentsInChildren<Collider>();
        currentHealth = maxHealth;
        pathing = GetComponent<EnemyPathing>();
    }

    private void Start() {
        GameObject health = Instantiate(WaveSpawner.ws_Single.mobileUiHealthPrefab, transform.position, Quaternion.identity);
        mobileUiHealth = health.GetComponent<MobileUiHealth>();
        mobileUiHealth.target = this;
        mobileUiHealth.transform.SetParent(MobileUiManager.um_single.mobileUiCanvas.transform);
    }

    public void Init() {
        pathing.Init();
        mobileUiHealth.Init();
    }
    
    public void DoDamage(float amount) {
        if(!isDead) {
            currentHealth -= amount;
            mobileUiHealth.UpdateValue(currentHealth / maxHealth);
            if(currentHealth <= 0) {
                Death();
            }
        }
    }

    public void Death() {
        mobileUiHealth.gameObject.SetActive(false);
        for (int i = 0; i < colliders.Length; i++) {
            colliders[i].enabled = false;
        }
        isDead = true;
        currentHealth = 0;
        anim.SetBool("Death", true);
        Invoke(nameof(Disable), destroyAfter);
    }

    void Disable() {
        isActive = false;
        gameObject.SetActive(false);
    }
}

public enum RobotType {
    normal,
    heavy,
    kamikaze
}