using System.Collections;
using UnityEngine;


namespace BrokenGears.Old {
    public class AlarmLight : MonoBehaviour {
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private int maxRepeat;
        [SerializeField] private float waitBetweenAlarms, spawnDelayAfterAlarm;

        private WaveSpawner spawner;

        public void SoundAlarm(WaveSpawner ws) {
            spawner = ws;
            StartCoroutine(Alarming());
        }

        private IEnumerator Alarming() {
            int timesRepeated = 0;
            while (timesRepeated < maxRepeat) {
                timesRepeated++;
                AudioManager.PlaySound(audioClip, AudioManager.AudioGroups.SFX, 1, transform.position);
                yield return new WaitForSeconds(audioClip.length + waitBetweenAlarms);
            }
            yield return new WaitForSeconds(spawnDelayAfterAlarm);
            spawner.StartSpawning();
        }
    }
}