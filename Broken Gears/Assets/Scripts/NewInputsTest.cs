using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenGears {
    public class NewInputsTest : MonoBehaviour {

        [SerializeField] private new Camera camera;
        [SerializeField] private float speed;

        private void Start() {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void FixedUpdate() {
            float x = InputManager.single.RotationDeltaAxis.x;
            float y = InputManager.single.RotationDeltaAxis.y;

            camera.transform.Rotate(Vector3.left * x * speed * Time.fixedDeltaTime);
            camera.transform.Rotate(Vector3.up * y * speed * Time.fixedDeltaTime, Space.World);
        }
    }
}