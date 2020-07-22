using UnityEngine;

public class OldSoundsManager : MonoBehaviour {

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
