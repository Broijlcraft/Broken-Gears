using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MobileUiHealth : MonoBehaviour {
    [SerializeField] private Image fillImage;
    private Enemy target;
    private bool isActive;

    #region Get/Set
    public void SetTarget(Enemy enemy) {
        target = enemy;
    }
    #endregion

    public void Init() {
        isActive = true;
    }

    private void Update() {
        if (isActive) {
            if (target) {
                transform.position = new Vector3(target.transform.position.x, target.transform.position.y + target.GetVerticalHealthBarOffSet(), target.transform.position.z);
            }
            transform.LookAt(Movement.m_Single.topdownCamera.transform);
        }
    }

    public void UpdateValue(float value) {
        fillImage.fillAmount = value;
    }

    private void OnDisable() {
        
    }
}
