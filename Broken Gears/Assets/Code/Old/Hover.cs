using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public string turretName;
    public int turretValue;

    public void OnPointerEnter(PointerEventData eventData) {
        if (!GameManager.gm_Single.rework) {
            OldUiManager.staticTurretText.text = turretName;
            OldUiManager.staticTurretValueText.text = "Cost: " + turretValue;
        }
    }
    public void OnPointerExit(PointerEventData eventData) {
        if (!GameManager.gm_Single.rework) {
            OldUiManager.staticTurretText.text = "";
            OldUiManager.staticTurretValueText.text = "";
        }
    }
}
