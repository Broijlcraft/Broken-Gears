//17-7-2020
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gm_Single;

    private void Awake() {
        gm_Single = this;
    }
}