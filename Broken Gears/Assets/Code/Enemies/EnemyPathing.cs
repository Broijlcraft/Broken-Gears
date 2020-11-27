using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    [SerializeField] private float speed, rotationSpeed, maxDistance = 0.1f, animationSpeed;
    [SerializeField] private GameObject enemyChild;
    [SerializeField] private Animator anim;

    private Enemy enemy;
    private bool isActive;
    private float defaultSpeed;
    private WaveSpawner spawner;
    private int targetValue = 0;
    private Transform targetPoint;
    
    #region Get/Set
    public float GetDefaultSpeed() {
        return defaultSpeed;
    }

    public void SetSpeed(float amount) {
        speed = amount;
    }
    #endregion

    private void Awake() {
        spawner = WaveSpawner.ws_Single;
    }

    private void Start() {
        enemy = GetComponent<Enemy>();
    }

    public void Init() {
        defaultSpeed = speed;
        targetValue = 0;
        SetTarget();
        isActive = true;
        if (anim) {
            anim.speed = animationSpeed;
        }
    }

    private void Update() {
        if (targetPoint != null && !enemy.GetIsDead() && isActive) {
            Vector3 direction = abs(targetPoint.position, transform.position);
            Vector3 directionToGo = targetPoint.position - transform.position;
            transform.Translate(directionToGo.normalized * speed * spawner.GetGlobalEnemySpeedMultiplier() * Time.deltaTime);
            if (direction.z < maxDistance && direction.x < maxDistance) {
                SetTarget();
            }
            Vector3 lookDir = targetPoint.position - enemyChild.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            Vector3 rotationToLook = Quaternion.Lerp(enemyChild.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
            enemyChild.transform.rotation = Quaternion.Euler(0f, rotationToLook.y, 0f);
        }
    }

    Vector3 abs(Vector3 v, Vector3 vA) {
        Vector3 vB = v - vA;
        float x = Mathf.Abs(vB.x);
        float y = Mathf.Abs(vB.y);
        float z = Mathf.Abs(vB.z);
        return new Vector3(x, y, z);
    }

    public void SetTarget() {
        if (Waypoints.wp_Single.waypoints.Count > targetValue) {
            targetPoint = Waypoints.wp_Single.waypoints[targetValue];
            targetValue++;
        } else {
            if (enemy && spawner.enemiesOnTheField.Contains(enemy)) {
                spawner.enemiesOnTheField.Remove(enemy);
            }
            WaveSpawner.ws_Single.IncreaseEscaped();
            enemy.Death(true);
        }
    }
}