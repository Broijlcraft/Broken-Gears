using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenGears.Old {
    public class SalvageTower : Tower {
        public static SalvageTower st_Single;
        public float scrapMultiplier = 2f;

        private void Awake() {
            st_Single = this;
        }

        public float GetScrapMultiplier() {
            float multi = 1;
            if (isActive) {
                multi = scrapMultiplier;
            }
            return multi;
        }
    }
}