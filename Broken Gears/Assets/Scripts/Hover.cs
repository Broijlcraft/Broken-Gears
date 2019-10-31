using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public string turretName;
    public int turretValue;

    public void OnPointerEnter(PointerEventData eventData) {
        UiManager.staticTurretText.text = turretName;
        UiManager.staticTurretValueText.text = "Cost: " + turretValue;
    }
    public void OnPointerExit(PointerEventData eventData) {
        UiManager.staticTurretText.text = "";
        UiManager.staticTurretValueText.text = "";
    }
}
