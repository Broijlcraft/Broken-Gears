using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField] private string towerName;
    [TextArea, SerializeField] private string description = "This is a description";
    [SerializeField] private Sprite towerSprite = null;
    [SerializeField] private ParticleSystem[] activeTowerParticles;
    [SerializeField] private int buyScrapPrice, sellScrapPrice;

    [SerializeField] private bool canNeverMove;

    protected bool isActive;
    protected TowerManager tManager;
    [HideInInspector] public Tile placedOnParentTile, oldParentTile;
    [HideInInspector] public List<Material> mats = new List<Material>();

    #region Get/Set
    public string GetTowerName() {
        return towerName;
    }

    public string GetDescription() {
        return description;
    }

    public Sprite GetTowerSprite() {
        return towerSprite;
    }

    public int GetBuyScrapPrice() {
        return buyScrapPrice;
    }

    public int GetSellScrapPrice() {
        return sellScrapPrice;
    }

    public bool GetCanNeverMove() {
        return canNeverMove;
    }
    #endregion

    private void Awake() {
        tManager = TowerManager.singleTM;
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
        tManager.SetSelectedTower(null);
        if (tileToPlaceOn) {
            placedOnParentTile = tileToPlaceOn;
            transform.position = placedOnParentTile.setPosition;
            transform.rotation = Quaternion.Euler(placedOnParentTile.setRotation);
        }
        tManager.SetSelectedTowerIsMoving(false);
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