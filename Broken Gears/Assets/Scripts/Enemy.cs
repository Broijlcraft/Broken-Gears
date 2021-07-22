using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenGears {
    public class Enemy : MonoBehaviour {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        [SerializeField] private float defaultSpeed;
        [SerializeField] private float currentSpeed;

        private Coroutine movement;

        private void OnEnable() {
            movement = StartCoroutine(Movement());
        }

        private void OnDestroy() {
            StopCoroutine(movement);
        }

        private IEnumerator Movement() {
            yield return new WaitForSeconds(0.1f);
        }
    }
}