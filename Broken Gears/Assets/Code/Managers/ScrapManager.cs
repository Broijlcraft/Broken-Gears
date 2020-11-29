using UnityEngine.UI;
using UnityEngine;

public class ScrapManager : MonoBehaviour {
    public static ScrapManager sm_single;

    [SerializeField] private int startScrapAmount, maxScrap;
    [SerializeField] private Text scrapTextObject;
    private int currentScrap;

    public enum ScrapOption {
        Add = 1,
        Withdraw = -1
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
        if (GameManager.gm_Single.DevMode()) {
            if (Input.GetButtonDown("Button1")) {
                AddOrWithdrawScrap(1, ScrapOption.Add);
            }
        }
    }

    public void Restart() {
        int difference = currentScrap - startScrapAmount;
        difference *= -1;

        int index = (int)Mathf.Sign(difference);
        ScrapOption option = (ScrapOption)index;

        difference = Mathf.Abs(difference);

        AddOrWithdrawScrap(difference, option);
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