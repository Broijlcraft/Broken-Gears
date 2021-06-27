using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace BrokenGears.Old {
    public class OptionsManager : MonoBehaviour {
        public AudioMixer audioMixer;
        public SettingSlider[] settingSliders;

        private void Start() {
            AudioManager.audioMixer = audioMixer;
            for (int i = 0; i < settingSliders.Length; i++) {
                SettingSlider ss = settingSliders[i];
                ss.slider.maxValue = ss.range.max;
                ss.slider.minValue = ss.range.min;
                ss.slider.wholeNumbers = false;

                //Cleanup after rework!!!
                if (ss.audioGroup != AudioManager.AudioGroups.None) {
                    switch (ss.audioGroup) {
                        case AudioManager.AudioGroups.Master:
                        ss.slider.onValueChanged.AddListener(OnMasterVolumeChanged);
                        break;
                        case AudioManager.AudioGroups.SFX:
                        ss.slider.onValueChanged.AddListener(OnSFXVolumeChanged);
                        break;
                        case AudioManager.AudioGroups.Music:
                        ss.slider.onValueChanged.AddListener(OnMusicVolumeChanged);
                        break;
                    }

                    ss.slider.value = PlayerPrefs.GetFloat(ss.audioGroup.ToString(), (ss.range.max + ss.range.min) / 2);
                } else {
                    if (ss.sensitivityType == SettingSlider.SensitivitySliders.MouseSensitivity) {
                        ss.slider.onValueChanged.AddListener(Movement.m_Single.OnMouseSensitivityChanged);
                    } else {
                        ss.slider.onValueChanged.AddListener(Movement.m_Single.OnZoomSensitivityChanged);
                    }

                    ss.slider.value = PlayerPrefs.GetFloat(ss.sensitivityType.ToString(), (ss.range.max + ss.range.min) / 2);
                }
                ss.slider.onValueChanged.AddListener(ss.OnSliderValueChanged);
            }
        }

        public void OnMasterVolumeChanged(float value) {
            AudioManager.audioMixer.SetFloat("MasterVolume", value);
        }

        public void OnSFXVolumeChanged(float value) {
            AudioManager.audioMixer.SetFloat("SFXVolume", value);
        }

        public void OnMusicVolumeChanged(float value) {
            AudioManager.audioMixer.SetFloat("MusicVolume", value);
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

        //Cleanup after rework!!!
        public void OnSliderValueChanged(float value) {
            if (audioGroup != AudioManager.AudioGroups.None) {
                PlayerPrefs.SetFloat(audioGroup.ToString(), value);
            } else {
                PlayerPrefs.SetFloat(sensitivityType.ToString(), value);
            }
        }
    }

    [Serializable]
    public class Range {
        public float max, min;
    }
}