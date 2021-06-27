using System.Collections.Generic;
using UnityEngine;

namespace BrokenGears.Old {
    public static class Tools {

        public static void EnableDisableGameObjectsFromArray(GameObject[] go, bool newState) {
            for (int i = 0; i < go.Length; i++) {
                if (go[i]) {
                    go[i].SetActive(newState);
                }
            }
        }

        public static void StartStopParticleSystemsFromArray(ParticleSystem[] systems, bool start) {
            for (int i = 0; i < systems.Length; i++) {
                if (start) {
                    systems[i].Play();
                } else {
                    systems[i].Stop();
                }
            }
        }

        public static Transform GetTarget(Enemy enemy) {
            Transform tp = enemy.transform;
            Transform enemyPoint = enemy.GetAttackTargetingPoint();
            if (enemyPoint) {
                tp = enemyPoint;
            }
            return tp;
        }

        public static List<Material> GetAllMaterialInstancesFromMeshRenderers(MeshRenderer[] renderers) {
            List<Material> mats = new List<Material>();
            for (int i = 0; i < renderers.Length; i++) {
                for (int iB = 0; iB < renderers[i].materials.Length; iB++) {
                    mats.Add(renderers[i].materials[iB]);
                }
            }
            return mats;
        }
    }
}