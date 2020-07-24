using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class OldHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public string turretName;
    public int turretValue;

    public void OnPointerEnter(PointerEventData eventData) {
        if (!GameManager.gm_Single.rework) {
            //OldUiManager.old_um_Single.turretText.text = turretName;
            //OldUiManager.old_um_Single.turretValueText.text = "Cost: " + turretValue;
        }
    }
    public void OnPointerExit(PointerEventData eventData) {
        if (!GameManager.gm_Single.rework) {
            //OldUiManager.old_um_Single.turretText.text = "";
            //OldUiManager.old_um_Single.turretValueText.text = "";
        }
    }
}
