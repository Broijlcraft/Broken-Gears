using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    public FullScreenMode mode;

    public Dropdown drop;

    private void Start() {
        drop.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < 4; i++) {
            string s = ((FullScreenMode)i).ToString();
            options.Add(s);
        }
        drop.AddOptions(options);
        drop.onValueChanged.AddListener(DropDown);
    }

    public void DropDown(int index) {
        int h = 1024;
        int w = 768;
        Screen.SetResolution(h, w, (FullScreenMode)index, 60);
    }
}