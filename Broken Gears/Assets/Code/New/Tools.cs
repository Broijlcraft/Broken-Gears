using UnityEngine;

public class Tools : MonoBehaviour{

    public static Tools tools;

    private void Awake() {
        tools = this;
    }

    public void EnableDisableGameObjectsFromArray(GameObject[] go, bool newState) {
        for (int i = 0; i < go.Length; i++) {
            if (go[i]) {
                go[i].SetActive(newState);
            }
        }
    }
}