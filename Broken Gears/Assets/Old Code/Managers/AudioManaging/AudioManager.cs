using UnityEngine.Audio;
using UnityEngine;

namespace BrokenGears.Old {
    public static class AudioManager {

        public static AudioMixer audioMixer;

        public enum AudioGroups {
            Master,
            Music,
            SFX,
            None
        }

        public static void PlaySound(AudioClip audioClipToPlay, AudioGroups audioGroups, float pitch, Vector3 position) {
            GameObject soundObject = new GameObject("Sound");
            soundObject.transform.position = position;
            AudioSource audio = soundObject.AddComponent<AudioSource>();
            audio.PlayOneShot(audioClipToPlay);
            audio.pitch = pitch;
            audio.outputAudioMixerGroup = audioMixer.FindMatchingGroups(audioGroups.ToString())[0];
            GameObject.Destroy(soundObject, audioClipToPlay.length);
        }
    }
}