using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    public string towerName;
    [TextArea]
    public string description = "This is a description";
    public Sprite towerSprite = null;
    public ParticleSystem[] activeTowerParticles;
    public int buyScrapPrice, sellScrapPrice;

    public bool canNeverMove;

    /*[HideInInspector]*/ public bool isActive;
    [HideInInspector] public Tile placedOnParentTile, oldParentTile;
    [HideInInspector] public List<Material> mats = new List<Material>();

    private void Awake() {
        SetMaterials();
        SetParticles();
    }

    public void SetMaterials() {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        mats = Tools.GetAllMaterialInstancesFromMeshRenderers(renderers);
        for (int i = 0; i < mats.Count; i++) {
            mats[i].EnableKeyword("_EmissionColor");
        }
    }

    public void SetParticles() {
        activeTowerParticles = GetComponentsInChildren<ParticleSystem>();
    }

    public void ChangeTowerColor(Color color) {
        for (int i = 0; i < mats.Count; i++) {
            mats[i].SetColor("_EmissionColor", color);
        }
    }

    public virtual void PlaceOnParentTile(Tile tileToPlaceOn) {
        TowerManager.tm_Single.selectedTower = null;
        if (tileToPlaceOn) {
            placedOnParentTile = tileToPlaceOn;
            transform.position = placedOnParentTile.setPosition;
            transform.rotation = Quaternion.Euler(placedOnParentTile.setRotation);
        }
        TowerManager.tm_Single.selectedTowerIsMoving = false;
        oldParentTile = null;
        ChangeTowerColor(Vector4.zero);
        isActive = true;
    }

    public virtual void DetachFromParentTile() {
        if (placedOnParentTile) {
            oldParentTile = placedOnParentTile;
            placedOnParentTile = null;
            oldParentTile.buildable = true;
            oldParentTile.buildableChild.buildable = true;
        }
        isActive = false;
    }
}