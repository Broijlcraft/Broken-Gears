using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadomizeSkinnedMaterial : MaterialRandomizerBase {
    [SerializeField] private SkinnedMeshAndMats[] skinnedMeshAndMats = new SkinnedMeshAndMats[1];

    public override void Init() {
        for (int i = 0; i < skinnedMeshAndMats.Length; i++) {
            Material[] sharedMaterials = GetRamdomizedMaterials(skinnedMeshAndMats[i].GetMats());
            SkinnedMeshRenderer[] renderers = skinnedMeshAndMats[i].GetSkinnedMeshRenderers();

            for (int iB = 0; iB < renderers.Length; iB++) {
                renderers[iB].sharedMaterials = sharedMaterials;
            }
        }
    }
}