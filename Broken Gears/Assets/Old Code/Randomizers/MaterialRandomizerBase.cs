using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenGears.Old {
    public class MaterialRandomizerBase : MonoBehaviour {

        public List<Material> currentRandomizedMaterials = new List<Material>();

        public virtual void Init() {
            currentRandomizedMaterials.Clear();
        }

        #region Get/Set
        private Material[] GetInstancedMaterials(Material[] materials) {
            List<Material> sharedMaterials = new List<Material>();
            for (int iB = 0; iB < materials.Length; iB++) {
                Material mat = new Material(materials[iB]);
                sharedMaterials.Add(mat);
            }
            return sharedMaterials.ToArray();
        }

        protected Material[] GetRamdomizedMaterials(Mats[] mats) {
            int rand = Random.Range(0, mats.Length);
            Material[] materials = mats[rand].GetSharedMaterials();
            materials = GetInstancedMaterials(materials);
            return materials;
        }

        public List<Material> GetCurrentRandomizedMaterials() {
            return currentRandomizedMaterials;
        }
        #endregion

        protected void AddMaterialToCurrentList(Material[] materials) {
            for (int i = 0; i < materials.Length; i++) {
                currentRandomizedMaterials.Add(materials[i]);
            }
        }

        #region Classes
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
        #endregion
    }
}