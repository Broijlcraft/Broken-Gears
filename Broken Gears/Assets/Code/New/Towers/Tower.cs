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

    private void Start() {
        print("start");
        SetMaterials();
    }

    public void SetMaterials() {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        print(meshRenderers.Length);
        for (int i = 0; i < meshRenderers.Length; i++) {
            mats.Add(meshRenderers[i].material);
            mats[i].EnableKeyword("_EmissionColor");
        }
    }

    public void ChangeTowerColor(Color color, bool useEmission) {
        print("change");
        for (int i = 0; i < mats.Count; i++) {
            //if (useEmission) {
            //    mats[i].EnableKeyword("_EmissionColor");
            //} else {
            //    mats[i].DisableKeyword("_EmissionColor");
            //}
            mats[i].SetColor("_EmissionColor", Color.cyan);
        }
    }

    public void PlaceOnTile(Tile tileToPlaceOn) {
        TowerManager.tm_Single.selectedTower = null;
        placedOnParentTile = tileToPlaceOn;
        oldParentTile = null;
        transform.position = placedOnParentTile.setPosition;
        transform.rotation = Quaternion.Euler(placedOnParentTile.setRotation);
        isActive = true;
    }
}