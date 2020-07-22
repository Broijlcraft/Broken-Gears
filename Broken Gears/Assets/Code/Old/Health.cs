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
        if (!GameManager.gm_Single.rework) {
            g = Instantiate(OldManager.healthSlider, transform.position + OldManager.healthSlider.GetComponent<MobileUiParts>().offSet, Quaternion.identity);
            g.transform.GetComponent<MobileUiParts>().parent = transform;
            g.transform.SetParent(OldManager.healthCanvas);
        }
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

    public void Death() {
        if (OldWaveSpawn.onTheField.Contains(gameObject)) {
            OldWaveSpawn.onTheField.Remove(gameObject);
        }
        if (!GameManager.gm_Single.rework) {
            gA = Instantiate(OldManager.scrapEconomy.scrapFab, transform.position + OldManager.scrapEconomy.scrapFab.GetComponent<MobileUiParts>().offSet, Quaternion.identity);
            gA.transform.GetComponent<MobileUiParts>().parent = transform;
            gA.transform.SetParent(OldManager.scrapCanvas);
            int value;
            value = scrapAdd;
            value += OldTowerManager.activeScrapTower;
            ScrapEconomy.AddScrap(value);
            gA.transform.GetComponentInChildren<Text>().text = "+ " + value;
        }
        GetComponent<EnemyPathing>().speed = 0;
        GetComponentInChildren<Animator>().SetBool("Death", true);
        GetComponent<EnemyPathing>().enemyChild.transform.SetParent(null);
        Destroy(g);
        Destroy(targetInemy);
        Destroy(GetComponent<EnemyPathing>().enemyChild, 3f);
        Destroy(gameObject);
    }
}
