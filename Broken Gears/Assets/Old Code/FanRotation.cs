using UnityEngine;

namespace BrokenGears.Old {
    public class FanRotation : MonoBehaviour {

        public Vector3 rot;
        public float speed;

        private void Update() {
            transform.Rotate(rot * speed * Time.deltaTime);
        }
    }
}
