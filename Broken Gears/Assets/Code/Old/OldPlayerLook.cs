using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class OldPlayerLook : MonoBehaviour {
    public static OldPlayerLook old_pl_single;

    public float mouseSensitivity, topLock, bottomLock, multiplier, xAxisClamp;

    public LayerMask layerMask;

    public Color defaultTextColor, goodButtonColor, badButtonTextColor, badButtonColor;

    Text buySellText;
    Button buySellButton;

    [Header("Animating")]

    public float speed;
    public bool moving;
    public int updates;
    public float amount;
    public bool canMove;
    public float initialMoveDelay;
    int i;
    Vector3 v;

    delegate void TowerFunctionOverload();

    private void Start() {
        //if (OldMenuScript.old_ms_Single.buySellText) {
        //    buySellText = OldMenuScript.old_ms_Single.buySellText.GetComponentInChildren<Text>();
        //    buySellButton = OldMenuScript.old_ms_Single.buySellText.GetComponent<Button>();
        //}
    }

    private void Update() {
        //RaycastHit hit;
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red * 1000);
        //if (!GameManager.gm_Single.rework && Input.GetMouseButtonDown(1) && OldMenuScript.old_ms_Single.menuState == OldMenuScript.MenuState.none && canMove == true) {
        //    Ray ray = OldManager.old_m_Single.cam.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
        //        if (hit.transform.gameObject.tag == "Turret" || hit.transform.gameObject.tag == "Scrap") {
        //            OldMenuScript.old_ms_Single.MenuSwitch("towerInteraction");
        //            if (hit.transform.tag == "Scrap") {
        //                if (hit.transform.GetComponent<OldSalvageTower>().bought == true) {
        //                    SetBuySellButton("Unlocked", false, Dummy, defaultTextColor, goodButtonColor);
        //                } else {
        //                    SetBuySellButton("Unlock for " + hit.transform.GetComponent<OldSalvageTower>().price, true, () => hit.transform.GetComponent<OldSalvageTower>().ActivateTower(), defaultTextColor, Color.white);
        //                }
        //            }
        //            if (hit.transform.tag == "Turret") {
        //                if (OldTowerManager.old_tm_Single.activeScrapTower > 0) {
        //                    SetBuySellButton("Salvage for " + hit.transform.GetComponentInParent<OldSelectTowerPlacement>().salvageValue, true, () => hit.transform.GetComponentInParent<OldSelectTowerPlacement>().SellTower(), defaultTextColor, Color.white);
        //                } else {
        //                    SetBuySellButton("Salvage Furnace Required", false, Dummy, badButtonTextColor, badButtonColor);
        //                }
        //            }
        //        }
        //    }
        //}
    }

    void Dummy() {

    }

    public void SellTower() {
        print("sell");
    }
    
    public void StartGame() {

    }

    void SetBuySellButton(string s, bool b, TowerFunctionOverload tfo, Color textColor, Color disabledButtonColor) {
        buySellText.text = s;
        buySellText.color = textColor;
        ColorBlock block = buySellButton.colors;
        block.disabledColor = disabledButtonColor;
        buySellButton.colors = block;
        buySellButton.interactable = b;
        buySellButton.onClick.RemoveAllListeners();
        buySellButton.onClick.AddListener(() => tfo());
    }
}