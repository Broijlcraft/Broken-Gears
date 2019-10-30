using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public string turretName;

    public void OnPointerEnter(PointerEventData eventData) {
        UiManager.staticTurretText.text = turretName;
    }
    public void OnPointerExit(PointerEventData eventData) {
        UiManager.staticTurretText.text = "";
    }
}
