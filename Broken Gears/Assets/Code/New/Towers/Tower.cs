using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    public string towerName;
    public ParticleSystem[] activeTowerParticles;
    public int buyScrapPrice, sellScrapPrice;
    [Header("Test for towerplacement and unlock")]
    public bool testEmmision;
    /*[HideInInspector] */public List<Material> mats = new List<Material>();
    public Tile placedOnParentTile, oldParentTile;
    public bool isActive;

    private void Awake() {
        SetMaterials();
    }

    public void SetMaterials() {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < meshRenderers.Length; i++) {
            mats.Add(meshRenderers[i].material);
            mats[i].EnableKeyword("_EmissionColor");
        }
    }

    public void ChangeTowerColor(Color color) {
        for (int i = 0; i < mats.Count; i++) {
            mats[i].SetColor("_EmissionColor", color);
        }
    }

    public void PlaceOnParentTile(Tile tileToPlaceOn) {
        TowerManager.tm_Single.selectedTower = null;
        placedOnParentTile = tileToPlaceOn;
        oldParentTile = null;
        ChangeTowerColor(Vector4.zero);
        isActive = true;
    }
}