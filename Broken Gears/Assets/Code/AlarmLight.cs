using System.Collections;
using UnityEngine;

public class AlarmLight : MonoBehaviour {
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private int maxRepeat;
    [SerializeField] private float waitBetweenAlarms, spawnDelayAfterAlarm;

    private int timesRepeated;
    private WaveSpawner spawner;

    public void SoundAlarm(WaveSpawner ws) {
        spawner = ws;
        StartCoroutine(Alarming());
    }

    private IEnumerator Alarming() {
        while (timesRepeated < maxRepeat) {
            timesRepeated++;
            AudioManager.PlaySound(audioClip, AudioManager.AudioGroups.SFX, 1, transform.position);
            yield return new WaitForSeconds(audioClip.length + waitBetweenAlarms);
        }
        yield return new WaitForSeconds(spawnDelayAfterAlarm);
        spawner.StartSpawning();
    }
}