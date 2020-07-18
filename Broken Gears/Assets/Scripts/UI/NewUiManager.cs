//17-7-2020
using UnityEngine.UI;
using UnityEngine;
using System;

public class NewUiManager : MonoBehaviour {
    public SettingSlider[] settingSliders;
}

[Serializable]
public class SettingSlider {
    public Slider slider;
    public Range range;
}

[Serializable]
public class Range {
    public float min, max;
}