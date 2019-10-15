using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        other.GetComponentInParent<EnemyPathing>().SetTarget();
    }
}
