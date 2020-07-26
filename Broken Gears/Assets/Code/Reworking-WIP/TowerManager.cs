using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {
    public static TowerManager tm_Single;
    
    private void Awake() {
        tm_Single = this;    
    }
}
