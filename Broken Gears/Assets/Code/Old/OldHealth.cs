using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OldHealth : MonoBehaviour {

    public float maxHealth;
    [HideInInspector] public float currentHealth;
    public int scrapAdd;
    public GameObject targetInemy;

    GameObject g, gA;

    private void Start() {
        currentHealth = maxHealth;
        if (!GameManager.gm_Single.devMode) {
            g = Instantiate(OldManager.old_m_Single.healthFab, transform.position + OldManager.old_m_Single.healthFab.GetComponent<OldMobileUiParts>().offSet, Quaternion.identity);
            g.transform.GetComponent<OldMobileUiParts>().parent = transform;
            g.transform.SetParent(OldManager.old_m_Single.healthCanvas);
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
        //if (OldWaveSpawn.old_wv_Single.onTheField.Contains(gameObject)) {
        //    OldWaveSpawn.old_wv_Single.onTheField.Remove(gameObject);
        //}
        //if (!GameManager.gm_Single.rework) {
        //    gA = Instantiate(OldScrapEconomy.old_se_Single.scrapFab, transform.position + OldScrapEconomy.old_se_Single.scrapFab.GetComponent<OldMobileUiParts>().offSet, Quaternion.identity);
        //    gA.transform.GetComponent<OldMobileUiParts>().parent = transform;
        //    gA.transform.SetParent(OldManager.old_m_Single.scrapCanvas);
        //    int value;
        //    value = scrapAdd;
        //    value += OldTowerManager.old_tm_Single.activeScrapTower;
        //    OldScrapEconomy.old_se_Single.AddScrap(value);
        //    gA.transform.GetComponentInChildren<Text>().text = "+ " + value;
        //}
        GetComponent<EnemyPathing>().speed = 0;
        GetComponentInChildren<Animator>().SetBool("Death", true);
        GetComponent<EnemyPathing>().enemyChild.transform.SetParent(null);
        Destroy(g);
        Destroy(targetInemy);
        Destroy(GetComponent<EnemyPathing>().enemyChild, 3f);
        Destroy(gameObject);
    }
}
