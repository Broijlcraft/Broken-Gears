using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalvageTower : MonoBehaviour {

    public bool bought;
    public int price;

    public GameObject vfx;

    public void ActivateTower() {
        print("Activate");
        vfx.SetActive(true);
    }

}
