using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour {

    public int maxHealth;
    int currentHealth;

    public GameObject healthPrefab;
    GameObject g;

    private void Start() {
        currentHealth = maxHealth;
        g = Instantiate(healthPrefab, Vector3.zero, Quaternion.identity);
        g.transform.SetParent(GameObject.Find("EnemyHealthCanvas").transform);
    }

    private void Update() {
        print(g.transform.Find("Fill").GetComponent<Image>().fillAmount);
    }

    private void LateUpdate() {
        g.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1f, 0));
    }

    public void Damage(int dmg) {
        currentHealth -= dmg;
        //g.transform.Find("Fill").GetComponent<Image>().fillAmount;
        if (currentHealth <= 0) {
            currentHealth = 0;
            Death();
        }
    }

    public void Death() {

    }
}
