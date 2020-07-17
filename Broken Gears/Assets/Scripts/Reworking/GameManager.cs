using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager gm_Single;

    private void Awake() {
        gm_Single = this;
    }
}