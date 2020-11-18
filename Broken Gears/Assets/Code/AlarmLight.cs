using UnityEngine;

public class AlarmLight : MonoBehaviour {
    public AudioClip audioClip;
    public int maxRepeat;
    public float waitBetweenAlarms, spawnDelayAfterAlarm;

    int timesRepeated;
    float timer;
    [HideInInspector] public bool soundAlarm;

    private void Update() {
        if (soundAlarm) {
            if(timesRepeated < maxRepeat) {
                if (timer == 0 || timer > audioClip.length + waitBetweenAlarms) {
                    timer = 0f;
                    timesRepeated++;
                    AudioManager.PlaySound(audioClip, AudioManager.AudioGroups.SFX, 1, transform.position);
                }
            } else {
                if(timer > audioClip.length + spawnDelayAfterAlarm) {
                    WaveSpawner ws = WaveSpawner.ws_Single;
                    ws.StartCoroutine(ws.Spawner());
                    soundAlarm = false;
                }
            }
            timer += Time.deltaTime;
        }
    }
}