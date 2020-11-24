using UnityEngine.UI;
using UnityEngine;

public class ScrapManager : MonoBehaviour {
    public static ScrapManager sm_single;

    [SerializeField] private int startScrapAmount, maxScrap;
    [SerializeField] private Text scrapTextObject;
    private int currentScrap;

    public enum ScrapOption {
        Add,
        Withdraw
    }

    #region Get/Set
    public int GetCurrentScrap() {
        return currentScrap;
    }
    #endregion

    private void Awake() {
        sm_single = this;
        currentScrap = 0;
    }

    private void Start() {
        AddOrWithdrawScrap(startScrapAmount, ScrapOption.Add);
        UpdateScrapAmount();
    }

    private void Update() {
        if (GameManager.gm_Single.devMode) {
            if (Input.GetButtonDown("Button1")) {
                AddOrWithdrawScrap(1, ScrapOption.Add);
            }
        }
    }

    public bool AddOrWithdrawScrap(int amount, ScrapOption option) {
        bool success = false;
        amount = Mathf.Abs(amount);
        if (amount > 0) {
            if(option == ScrapOption.Add) {
                if(currentScrap < maxScrap) {
                    currentScrap += amount;
                    if(currentScrap > maxScrap) {
                        currentScrap = maxScrap;
                    }
                    success = true;
                }
            } else {
                if(currentScrap > 0) {
                    currentScrap -= amount;
                    if(currentScrap < 0) {
                        currentScrap = 0;
                    }
                    success = true;
                }
            }
        }
        UpdateScrapAmount();
        TowerManager.singleTM.CheckPricesSetInteractableAndNot();
        return success;
    }

    void UpdateScrapAmount() {
        scrapTextObject.text = currentScrap.ToString();
    }
}