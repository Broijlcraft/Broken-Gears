﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadomizeSkinnedMaterial : MaterialRandomizerBase {
    [SerializeField] private SkinnedMeshAndMats[] skinnedMeshAndMats = new SkinnedMeshAndMats[1];

    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            Init();
        }
    }

    public override void Init() {
        base.Init();
        for (int i = 0; i < skinnedMeshAndMats.Length; i++) {
            Material[] sharedMaterials = GetRamdomizedMaterials(skinnedMeshAndMats[i].GetMats());
            AddMaterialToCurrentList(sharedMaterials);
            SkinnedMeshRenderer[] renderers = skinnedMeshAndMats[i].GetSkinnedMeshRenderers();

            for (int iB = 0; iB < renderers.Length; iB++) {
                renderers[iB].sharedMaterials = sharedMaterials;
            }
        }
    }

    public Material[] GetMaterialsFromRenderers() {
        return new Material[0];
    }
}