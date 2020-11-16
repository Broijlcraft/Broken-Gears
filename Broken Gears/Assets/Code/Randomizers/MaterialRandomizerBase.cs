using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialRandomizerBase : MonoBehaviour {

    public virtual void Init() { }
       
    protected Material[] GetRamdomizedMaterials(Mats[] mats) {
        int rand = Random.Range(0, mats.Length);
        return mats[rand].GetSharedMaterials();
    }

    [System.Serializable]
    protected class MeshAndMats {
        [SerializeField] private string meshName = $"Example: {"DoorFrame"}";
        [SerializeField] private MeshRenderer[] renderers = new MeshRenderer[1];
        [SerializeField] private Mats[] mats = new Mats[1];

        public Mats[] GetMats() {
            return mats;
        }

        public MeshRenderer[] GetMeshRenderers() {
            return renderers;
        }
    }

    [System.Serializable]
    protected class SkinnedMeshAndMats {
        [SerializeField] private string meshName = $"Example: {"DoorFrame"}";
        [SerializeField] private SkinnedMeshRenderer[] skinnedRenderers = new SkinnedMeshRenderer[1];
        [SerializeField] private Mats[] mats = new Mats[1];

        public Mats[] GetMats() {
            return mats;
        }

        public SkinnedMeshRenderer[] GetSkinnedMeshRenderers() {
            return skinnedRenderers;
        }
    }

    [System.Serializable]
    protected class Mats {
        [SerializeField] private string materialName = $"Example: {"Dirty"}";
        [SerializeField] private Material[] sharedMaterials = new Material[1];

        public Material[] GetSharedMaterials() {
            return sharedMaterials;
        }
    }
}