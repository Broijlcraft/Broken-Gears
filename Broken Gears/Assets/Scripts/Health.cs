﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour {

    public float maxHealth;
    public float currentHealth;

    public GameObject healthPrefab;
    GameObject g;

    private void Start() {
        currentHealth = maxHealth;
        g = Instantiate(healthPrefab, Vector3.zero, Quaternion.identity);
        g.transform.SetParent(Manager.mobileCanvas.transform);
    }

    private void Update() {
        Damage(0); 
    }

    private void LateUpdate() {
        g.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1f, 0));
    }

    public void Damage(int dmg) {
        currentHealth -= dmg;
        g.transform.Find("Fill").GetComponent<Image>().fillAmount = currentHealth/maxHealth;
        if (currentHealth <= 0) {
            currentHealth = 0;
            Death();
        }
    }

    public void Death() {
        //death animation
        Instantiate(ScrapEconomy.scrap, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
}
