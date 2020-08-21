using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MobileUiScrap : MonoBehaviour {

    public float destroyAfter;
    public Text scrapText;

    private void Start() {
        Destroy(gameObject, destroyAfter);
    }

    public void SetValue(int value) {
        scrapText.text = value.ToString();
    }

    void Update() {
        transform.LookAt(Movement.m_Single.topdownCamera.transform);
    }
}
