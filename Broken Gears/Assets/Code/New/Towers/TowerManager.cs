using UnityEngine;

public class TowerManager : MonoBehaviour {
    public static TowerManager tm_Single;
    public LayerMask layersToIgnore = (1 << 8);

    [Header("HideInInspector")]
    public Tower selectedTower;

    private void Awake() {
        tm_Single = this;
    }
}
