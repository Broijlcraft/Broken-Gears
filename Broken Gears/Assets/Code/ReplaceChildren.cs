using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ReplaceChildren : MonoBehaviour {
    public Transform parent;
    public GameObject prefab;

    private void Reset() {
        parent = this.transform;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ReplaceChildren))]
public class RpEditor : Editor {
    ReplaceChildren rpScript;

    private void OnEnable() {
        rpScript = (ReplaceChildren)target;
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if (GUILayout.Button("Replace")) {
            if(rpScript.parent && rpScript.prefab) {
                Debug.LogWarning("Replacing all children with: " + rpScript.prefab);
                List<Transform> oldObject = new List<Transform>();
                for (int i = 0; i < rpScript.transform.childCount; i++) {
                    oldObject.Add(rpScript.transform.GetChild(i));
                }
                for (int i = 0; i < oldObject.Count; i++) {
                    Object go = PrefabUtility.InstantiatePrefab(rpScript.prefab);
                    GameObject g = go as GameObject;
                    g.transform.position = oldObject[i].position;
                    g.transform.rotation = oldObject[i].rotation;
                    g.transform.localScale = oldObject[i].localScale;
                    g.transform.SetParent(rpScript.parent);
                    g.name += $" ({i})";
                    DestroyImmediate(oldObject[i].gameObject);
                    EditorUtility.SetDirty(g);
                }
                Debug.LogWarning("Done");
                DestroyImmediate(rpScript);
            }
        }
    }
}
#endif