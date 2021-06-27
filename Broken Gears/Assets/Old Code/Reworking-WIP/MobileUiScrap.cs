using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace BrokenGears.Old {
    public class MobileUiScrap : MonoBehaviour {

        [SerializeField] private float disableAfter;
        [SerializeField] private Text scrapText;

        [SerializeField] private Animator anim;

        private Transform lookAt;

        public void Init(Transform target) {
            lookAt = target;
            gameObject.SetActive(false);
        }

        void Update() {
            transform.LookAt(lookAt);
        }

        public void Reset() {
            anim.SetTrigger("Reset");
        }

        public void SetValueAndEnable(int value, Vector3 position) {
            transform.position = position;
            transform.LookAt(lookAt);

            scrapText.text = value.ToString();
            gameObject.SetActive(true);
            anim.SetTrigger("Fade");

            Invoke(nameof(Disable), disableAfter);
        }

        void Disable() {
            gameObject.SetActive(false);
        }
    }
}