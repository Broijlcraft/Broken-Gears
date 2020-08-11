using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    public string towerName, description = "This is a description";
    public Sprite towerSprite;
    public ParticleSystem[] activeTowerParticles;
    public int buyScrapPrice, sellScrapPrice;

    [HideInInspector] public bool isActive;
    [HideInInspector] public Tile placedOnParentTile, oldParentTile;
    [HideInInspector] public List<Material> mats = new List<Material>();

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

    public virtual void PlaceOnParentTile(Tile tileToPlaceOn) {
        TowerManager.tm_Single.selectedTower = null;
        placedOnParentTile = tileToPlaceOn;
        transform.position = placedOnParentTile.setPosition;
        transform.rotation = Quaternion.Euler(placedOnParentTile.setRotation);
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