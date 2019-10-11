﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour {

    public float maxHealth;
    public float currentHealth;

    public int scrapAdd;

    GameObject g;

    private void Start() {
        currentHealth = maxHealth;    
        g = Instantiate(Manager.helthTest, Vector3.zero, Quaternion.identity);
        g.transform.SetParent(Manager.mobileCanvas);
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            //Damage(Mathf.RoundToInt(maxHealth * 0.25f));
            Death();
        }
    }

    private void LateUpdate() {
        g.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f, 0));
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
        //GameObject gA = Instantiate(Manager.scrapEconomy.scrapFab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
        //gA.transform.SetParent(Manager.mobileCanvas);
        //gA.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        //gA.GetComponentInChildren<Text>().text = "+" + scrapAdd;
        ScrapEconomy.AddScrap(scrapAdd);
        Destroy(g);
        Destroy(gameObject);
    }
}
