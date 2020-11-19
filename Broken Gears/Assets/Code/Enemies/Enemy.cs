using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public RobotType robotType;
    public Transform attackTargetingPoint;
    public float maxHealth, destroyAfter, verticalHealthBarOffSet;
    public Range scrapDroppedOnDeathBetween;
    public float currentHealth;

    private Animator anim;
    private bool isActive;
    private Collider[] colliders;
    private MobileUiHealth mobileUiHealth;
    private bool isDead;
    private EnemyPathing pathing;

    private MaterialRandomizerBase randomizer;

    private WaveSpawner spawner;

    #region Get/Set

    public bool GetIsDead() {
        return isDead;
    }

    public EnemyPathing GetEnemyPathing() {
        return pathing;
    }

    #endregion


    private void Awake() {
        anim = GetComponentInChildren<Animator>();
        colliders = GetComponentsInChildren<Collider>();
        pathing = GetComponent<EnemyPathing>();
        randomizer = GetComponent<MaterialRandomizerBase>();

        if (CheckForSpawner()) {
            GameObject health = Instantiate(spawner.mobileUiHealthPrefab, transform.position, Quaternion.identity);
            mobileUiHealth = health.GetComponent<MobileUiHealth>();
            mobileUiHealth.target = this;
            mobileUiHealth.transform.SetParent(MobileUiManager.um_single.mobileUiCanvas.transform);
        }
    }

    public void Init() {
        pathing.Init();
        mobileUiHealth.Init();
        currentHealth = maxHealth;

        if (randomizer) {
            randomizer.Init();
        }
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
        if (CheckForSpawner()) {
            spawner.enemiesOnTheField.Remove(this);
        }
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

    bool CheckForSpawner() {
        bool hasSpawner = false;
        if (spawner) {
            hasSpawner = true;
        } else {
            spawner = WaveSpawner.ws_Single;
            hasSpawner = spawner;
        }
        return hasSpawner;
    }
}

public enum RobotType {
    normal,
    heavy,
    kamikaze
}