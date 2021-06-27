using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace BrokenGears.Old {
    public class MobileUiManager : MonoBehaviour {
        public static MobileUiManager um_single;

        public Canvas mobileUiCanvas, scrapCanvas;

        private void Awake() {
            um_single = this;
        }

        private void Start() {
            mobileUiCanvas.worldCamera = Movement.m_Single.topdownCamera;
        }
    }
}