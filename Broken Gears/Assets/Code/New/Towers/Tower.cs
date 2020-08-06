using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    public string towerName;
    public ParticleSystem[] activeTowerParticles;
    [Header("Test for towerplacement and unlock")]
    public bool testEmmision;

    [HideInInspector] public List<Material> mats = new List<Material>();
}
