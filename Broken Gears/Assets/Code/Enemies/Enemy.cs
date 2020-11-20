using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    [SerializeField] private RobotType robotType;
    [SerializeField] private Transform attackTargetingPoint;
    [SerializeField] private float maxHealth, disableAfter, verticalHealthBarOffSet;
    [SerializeField] private Range scrapDroppedOnDeathBetween;

    private bool isDead;
    private Animator anim;
    private float currentHealth;
    private WaveSpawner spawner;
    private Collider[] colliders;
    private EnemyPathing pathing;
    private MobileUiHealth mobileUiHealth;
    private MaterialRandomizerBase randomizer;
    
    #region Get/Set
    public float GetVerticalHealthBarOffSet() {
        return verticalHealthBarOffSet;
    }

    public bool GetIsDead() {
        return isDead;
    }

    public EnemyPathing GetEnemyPathing() {
        return pathing;
    }

    public Transform GetAttackTargetingPoint() {
        return attackTargetingPoint;
    }
    #endregion


    private void Awake() {
        anim = GetComponentInChildren<Animator>();
        colliders = GetComponentsInChildren<Collider>();
        pathing = GetComponent<EnemyPathing>();
        randomizer = GetComponent<MaterialRandomizerBase>();

        if (CheckForSpawner()) {
            GameObject health = Instantiate(spawner.GetMobileUiHealtPrefab(), transform.position, Quaternion.identity);
            mobileUiHealth = health.GetComponent<MobileUiHealth>();
            mobileUiHealth.SetTarget(this);
            mobileUiHealth.transform.SetParent(MobileUiManager.um_single.mobileUiCanvas.transform);
        }
    }

    public void Init() {
        if (randomizer) {
            randomizer.Init();
        }
        pathing.Init();
        mobileUiHealth.Init();
        currentHealth = maxHealth;
    }
    
    public void DoDamage(float amount) {
        if(!isDead) {
            currentHealth -= amount;
            mobileUiHealth.UpdateValue(currentHealth / maxHealth);
            if(currentHealth <= 0) {
                Death(false);
            }
        } else {
            print(isDead);
        }
    }

    public void Death(bool instant) {
        if (CheckForSpawner()) {
            spawner.enemiesOnTheField.Remove(this);
        }
        mobileUiHealth.gameObject.SetActive(false);
        for (int i = 0; i < colliders.Length; i++) {
            colliders[i].enabled = false;
        }
        isDead = true;
        currentHealth = 0;
        if (!instant) {
            anim.SetBool("Death", true);
        }
        Invoke(nameof(Disable), disableAfter);
    }

    void Disable() {
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