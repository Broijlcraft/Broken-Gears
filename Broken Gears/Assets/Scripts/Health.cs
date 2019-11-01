using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour {

    public float maxHealth;
    [HideInInspector] public float currentHealth;
    public int scrapAdd;
    public GameObject targetInemy;

    GameObject g;
    GameObject gA;

    private void Start() {
        currentHealth = maxHealth;
        g = Instantiate(Manager.healthSlider, transform.position + Manager.healthSlider.GetComponent<MobileUiParts>().offSet, Quaternion.identity);
        g.transform.GetComponent<MobileUiParts>().parent = transform;
        g.transform.SetParent(Manager.healthCanvas);
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            Death();
        }
    }

    public void Damage(int dmg) {
        currentHealth -= dmg;
        if (g != null) {
            g.transform.Find("Fill").GetComponent<Image>().fillAmount = currentHealth / maxHealth;
        }
        if (currentHealth <= 0) {
            currentHealth = 0;
            Death();
        }
    }

    void ValueChange() {

    }

    public void Death() {
        //death animation
        gA = Instantiate(Manager.scrapEconomy.scrapFab, transform.position + Manager.scrapEconomy.scrapFab.GetComponent<MobileUiParts>().offSet, Quaternion.identity);
        gA.transform.GetComponent<MobileUiParts>().parent = transform;
        gA.transform.SetParent(Manager.scrapCanvas);
        int value;
        value = scrapAdd;
        value += TowerManager.activeScrapTower;
        gA.transform.GetComponentInChildren<Text>().text = "+ " + value;
        ScrapEconomy.AddScrap(value);
        GetComponent<EnemyPathing>().speed = 0;
        GetComponentInChildren<Animator>().SetBool("Death", true);
        GetComponent<EnemyPathing>().enemyChild.transform.SetParent(null);
        Destroy(g);
        Destroy(targetInemy);
        Destroy(GetComponent<EnemyPathing>().enemyChild, 3f);
        Destroy(gameObject);
    }
}
