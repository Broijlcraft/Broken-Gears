using UnityEngine;

public class Enemy : MonoBehaviour {
    public Transform attackTargetingPoint;
    public float maxHealth, destroyAfter, verticalHealthBarOffSet;
    public Range scrapDroppedOnDeathBetween;

    Animator anim;
    public float currentHealth;
    Collider[] colliders;
    MobileUiHealth mobileUiHealth;
    [HideInInspector] public bool isDead;
    [HideInInspector] public EnemyPathing pathfinding;

    private void Awake() {
        anim = GetComponentInChildren<Animator>();
        colliders = GetComponentsInChildren<Collider>();
        currentHealth = maxHealth;
        pathfinding = GetComponent<EnemyPathing>();
    }

    private void Start() {
        GameObject health = Instantiate(WaveSpawner.ws_Single.mobileUiHealthPrefab, transform.position, Quaternion.identity);
        mobileUiHealth = health.GetComponent<MobileUiHealth>();
        mobileUiHealth.target = this;
        mobileUiHealth.transform.SetParent(MobileUiManager.um_single.mobileUiCanvas.transform);
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
        Destroy(mobileUiHealth.gameObject);
        for (int i = 0; i < colliders.Length; i++) {
            colliders[i].enabled = false;
        }
        isDead = true;
        currentHealth = 0;
        anim.SetBool("Death", true);
        Destroy(gameObject, destroyAfter);
    }
}