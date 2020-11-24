using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
    [SerializeField] private RobotType robotType;
    [SerializeField] private Transform attackTargetingPoint;
    [SerializeField] private float maxHealth, disableAfter, verticalHealthBarOffSet;
    [SerializeField] private Range scrapDroppedOnDeathBetween;

    [SerializeField] private float fadeTime, fadeSmooth;
    [SerializeField] private List<Material> fadeMats = new List<Material>();

    private bool isDead;
    private Animator anim;
    private float currentHealth;
    private WaveSpawner spawner;
    private Collider[] colliders;
    private EnemyPathing pathing;
    private MobileUiHealth mobileUiHealth;
    public MaterialRandomizerBase randomizer;

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

        if (!randomizer) {
            MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++) {
                Material[] mats = renderers[i].sharedMaterials;
                for (int iB = 0; iB < mats.Length; iB++) {
                    Material mat = new Material(mats[iB]);
                    fadeMats.Add(mat);
                }
            }
        }

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
        if (!isDead) {
            currentHealth -= amount;
            mobileUiHealth.UpdateValue(currentHealth / maxHealth);
            if (currentHealth <= 0) {
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
        if (mobileUiHealth) {
            mobileUiHealth.gameObject.SetActive(false);
        }
        for (int i = 0; i < colliders.Length; i++) {
            colliders[i].enabled = false;
        }
        isDead = true;
        currentHealth = 0;
        if (!instant) {
            anim.SetBool("Death", true);
        }
        anim.speed = 1;
        
        StartFading();
        Invoke(nameof(Disable), disableAfter);
    }

    void Disable() {
        gameObject.SetActive(false);
    }

    bool CheckForSpawner() {
        bool hasSpawner;
        if (spawner) {
            hasSpawner = true;
        } else {
            spawner = WaveSpawner.ws_Single;
            hasSpawner = spawner;
        }
        return hasSpawner;
    }

    void StartFading() {
        if (randomizer) { 
            fadeMats = randomizer.GetCurrentRandomizedMaterials();
        }

        InvokeRepeating(nameof(Fade), fadeTime, 1f/fadeSmooth);
    }
    
    void Fade() {
        for (int i = 0; i < fadeMats.Count; i++) {
            Material material = fadeMats[i];
            Color color = material.color;
            color.a = 1-1/fadeSmooth;
            material.color = color;
        }
    }

    void DropScrap() {
        Range r = scrapDroppedOnDeathBetween;
        int amount = Random.Range((int)r.min, (int)r.max);
        ScrapManager.sm_single.AddOrWithdrawScrap(amount, ScrapManager.ScrapOption.Add);
    }
}

public enum RobotType {
    normal,
    heavy,
    kamikaze
}