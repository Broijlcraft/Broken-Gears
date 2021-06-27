using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BrokenGears.Old {
    public class Enemy : MonoBehaviour {
        [SerializeField] private RobotType robotType;
        [SerializeField] private Transform attackTargetingPoint;
        [SerializeField] private float maxHealth, disableAfter, verticalHealthBarOffSet;
        [SerializeField] private Range scrapDroppedOnDeathBetween;
        [SerializeField] private GameObject scrapPrefab;
        [SerializeField] private List<ParticleSystem> particleSystems = new List<ParticleSystem>();

        private bool isDead;
        private Animator anim;
        private float currentHealth;
        private WaveSpawner spawner;
        private Collider[] colliders;
        private EnemyPathing pathing;
        private MobileUiHealth mobileUiHealth;
        private MaterialRandomizerBase randomizer;
        [SerializeField] private MobileUiScrap scrap;

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

        public RobotType GetRobotType() {
            return robotType;
        }
        #endregion

        private void Awake() {
            anim = GetComponentInChildren<Animator>();
            colliders = GetComponentsInChildren<Collider>();
            pathing = GetComponent<EnemyPathing>();
            randomizer = GetComponent<MaterialRandomizerBase>();

            scrapPrefab = Instantiate(scrapPrefab, MobileUiManager.um_single.scrapCanvas.transform);
            scrap = scrapPrefab.GetComponent<MobileUiScrap>();
            scrap.Init(Movement.m_Single.topdownCamera.transform);

            if (CheckForSpawner()) {
                GameObject health = Instantiate(spawner.GetMobileUiHealtPrefab(), transform.position, Quaternion.identity);
                mobileUiHealth = health.GetComponent<MobileUiHealth>();
                mobileUiHealth.SetTarget(this);
                mobileUiHealth.transform.SetParent(MobileUiManager.um_single.mobileUiCanvas.transform);
                mobileUiHealth.gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
        }

        public void Init() {
            transform.localPosition = Vector3.zero;
            isDead = false;
            if (randomizer) {
                randomizer.Init();
            }
            pathing.Init();
            currentHealth = maxHealth;
            mobileUiHealth.Init();
        }

        public void DoDamage(float amount) {
            if (!isDead) {
                currentHealth -= amount;
                mobileUiHealth.UpdateValue(currentHealth / maxHealth);
                if (currentHealth <= 0) {
                    Death(false);
                }
            }
        }

        public void Death(bool instant) {
            isDead = true;
            currentHealth = 0;

            for (int i = 0; i < particleSystems.Count; i++) {
                particleSystems[i].Stop();
            }

            if (CheckForSpawner()) {
                spawner.RemoveEnemy(this);
            }

            if (mobileUiHealth) {
                mobileUiHealth.gameObject.SetActive(false);
            }

            for (int i = 0; i < colliders.Length; i++) {
                colliders[i].enabled = false;
            }

            if (!instant) {
                anim.speed = 1;
                anim.SetBool("Death", true);
                DropScrap();
                Invoke(nameof(Disable), disableAfter);
            } else {
                Disable();
            }
        }

        void Disable() {
            gameObject.SetActive(false);
        }

        bool CheckForSpawner() {
            bool hasSpawner;
            if (spawner) {
                hasSpawner = true;
            } else {
                spawner = WaveSpawner.singleWS;
                hasSpawner = spawner;
            }
            return hasSpawner;
        }

        void DropScrap() {
            Range r = scrapDroppedOnDeathBetween;
            int amount = Random.Range((int)r.min, (int)r.max);
            scrap.SetValueAndEnable(amount, transform.position);
            ScrapManager.sm_single.AddOrWithdrawScrap(amount, ScrapManager.ScrapOption.Add);
        }
    }

    public enum RobotType {
        normal,
        heavy,
        kamikaze
    }
}