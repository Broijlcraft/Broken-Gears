using UnityEngine;
using UnityEngine.UI;

public class OldScrapEconomy : MonoBehaviour {

    public static OldScrapEconomy old_se_Single;

    public GameObject scrapFab;
    public int startScrap;
    public int currentScrap;
    public string scrapText;

    public static Text uiScrap;

    private void Awake() {
        old_se_Single = this;
    }

    private void Start() {
        uiScrap = OldManager.old_m_Single.canvasTest.Find("HUD").Find("Img_Scrap").GetComponentInChildren<Text>();
        currentScrap = startScrap;
        ScrapUpdate();
    }

    public void AddScrap(int i) {
        currentScrap += i;
        ScrapUpdate();
    }

    public void RemoveScrap(int i) {
        currentScrap -= i;
        ScrapUpdate();
    }

    public void ScrapUpdate() {
        uiScrap.text = (scrapText + " " + currentScrap);
    }
}
