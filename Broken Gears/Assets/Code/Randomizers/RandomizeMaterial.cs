using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeMaterial : MaterialRandomizerBase {
    [SerializeField] protected MeshAndMats[] meshAndMats = new MeshAndMats[1];

    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            Init();
        }
    }

    public override void Init() {
        for (int i = 0; i < meshAndMats.Length; i++) {
            Material[] sharedMaterials = GetRamdomizedMaterials(meshAndMats[i].GetMats());
            MeshRenderer[] renderers = meshAndMats[i].GetMeshRenderers();

            for (int iB = 0; iB < renderers.Length; iB++) {
                renderers[iB].sharedMaterials = sharedMaterials;
            }
        }
    }
}