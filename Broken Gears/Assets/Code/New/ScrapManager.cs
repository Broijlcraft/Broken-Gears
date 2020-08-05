﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScrapManager : MonoBehaviour {
    public static ScrapManager sm_single;

    public int startScrapAmount, maxScrap;
    public Text scrapTextObject;
    [Header("private / HideInInspector")] public int currentScrap;

    public enum ScrapOption {
        Add,
        Withdraw
    }

    private void Awake() {
        sm_single = this;
    }

    private void Start() {
        currentScrap = startScrapAmount;
        UpdateScrapAmount();
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
        return success;
    }

    void UpdateScrapAmount() {
        scrapTextObject.text = currentScrap.ToString();
    }
}