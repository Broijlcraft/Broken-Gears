using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenGears.Old {
    public class OldSalvageTower : MonoBehaviour {

        public bool bought;
        public int price;

        public GameObject vfx;

        public void ActivateTower() {
            bought = true;
            //OldTowerManager.old_tm_Single.activeScrapTower++;
            vfx.SetActive(true);
            //OldMenuScript.old_ms_Single.MenuSwitch("none");
            //OldScrapEconomy.old_se_Single.RemoveScrap(price);
        }
    }
}