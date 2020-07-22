using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    public Transform targetPoint;
    public float speed;
    [HideInInspector] public float defaultSpeed;
    public float rotationSpeed;
    int targetValue = 0;
    public float maxDistance = 0.1f;
    public GameObject enemyChild;

    private void Start() {
        defaultSpeed = speed;
        SetTarget();
    }

    private void Update() {
        if (targetPoint != null) {
            Vector3 direction = abs(targetPoint.position, transform.position);
            Vector3 directionToGo = targetPoint.position - transform.position;
            transform.Translate(directionToGo.normalized * speed * Time.deltaTime);
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
        if (Waypoints.wp_Single.waypoints.Length > targetValue) {
            targetPoint = Waypoints.wp_Single.waypoints[targetValue];
            targetValue++;
        } else {
            if (!GameManager.gm_Single.rework) {
                OldManager.uiManager.IncreaseEscaped(1);
            }
            Destroy(gameObject);
        }
    }
}