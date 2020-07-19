//17-7-2020
using UnityEngine.UI;
using UnityEngine;
using System;

public class OptionsManager : MonoBehaviour {
    public SettingSlider[] settingSliders;

    private void Start() {
        for (int i = 0; i < settingSliders.Length; i++) {
            SettingSlider ss = settingSliders[i];
            ss.slider.maxValue = ss.range.max;
            ss.slider.minValue = ss.range.min;
            ss.slider.wholeNumbers = false;
            //Cleanup after rework!!!
            if(ss.audioGroup != AudioManager.AudioGroups.None) {
                ss.slider.value = PlayerPrefs.GetFloat(ss.audioGroup.ToString(), (ss.range.max + ss.range.min) / 2);
            } else {
                ss.slider.value = PlayerPrefs.GetFloat(ss.sensitivityType.ToString(), (ss.range.max + ss.range.min) / 2);
            }
        }
    }
}

[Serializable]
public class SettingSlider {
    public Slider slider;
    public Range range;

    [Header("This will be cleaned up after rework")]
    public AudioManager.AudioGroups audioGroup;
    public enum SensitivitySliders {
        MouseSensitivity,
        ZoomSensitivity
    }
    public SensitivitySliders sensitivityType;
}

[Serializable]
public class Range {
    public float min, max;
}