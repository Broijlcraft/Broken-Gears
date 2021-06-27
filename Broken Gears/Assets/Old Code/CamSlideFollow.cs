using UnityEngine;

namespace BrokenGears.Old {
    public class CamSlideFollow : MonoBehaviour {

        private void FixedUpdate() {
            Vector3 newPos = transform.position;
            newPos.z = Movement.m_Single.transform.position.z;
            transform.position = newPos;
        }
    }
}