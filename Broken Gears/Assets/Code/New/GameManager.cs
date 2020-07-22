using UnityEngine;

public class GameManager : MonoBehaviour {
    public bool rework = true;
    public static GameManager gm_Single;

    private void Awake() {
        gm_Single = this;
    }
}