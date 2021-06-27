using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace BrokenGears.Old {
    public class ObjectPooler : MonoBehaviour {
        public List<GameObject> robots = new List<GameObject>();
        public int maxRobotsPerType = 5;

        public Dictionary<string, GameObject> robotPool = new Dictionary<string, GameObject>();

        public GameObject SpawnObject(string tag) {
            GameObject obj = null;

            return obj;
        }
    }
}