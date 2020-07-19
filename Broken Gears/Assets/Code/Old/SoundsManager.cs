using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour {

    public AudioClip alarm;
    public AudioClip backgroundMusic;

    public AudioSource backgroudMusicSource;

    private void Start() {
        backgroudMusicSource.clip = backgroundMusic;
        backgroudMusicSource.Play();
    }

    public void PlayAlarm() {

    }

}
