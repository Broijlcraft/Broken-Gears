using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MobileUiHealth : MonoBehaviour {
    public Image fillImage;
    [HideInInspector] public Enemy target;

    private void Update() {
        if (target) {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y + target.verticalHealthBarOffSet, target.transform.position.z);
        }
        transform.LookAt(Movement.m_Single.topdownCamera.transform);
    }

    public void UpdateValue(float value) {
        fillImage.fillAmount = value;
    }
}
