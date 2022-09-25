namespace BrokenGears.Combat {
    using UnityEngine;

    public class SawGun : AWeaponizedTurret {
        [SerializeField] private Bone blade;

        protected override void Update() {
            base.Update();

            if (blade.Origin && target != defaultTarget) {
                RotateBlade();
            }
        }

        private void RotateBlade() {
            Vector3 direction = Vector3.one;

            direction.x *= IsAxisRotation(Axis.x);
            direction.y *= IsAxisRotation(Axis.y);
            direction.z *= IsAxisRotation(Axis.z);

            blade.Origin.Rotate(direction * Time.deltaTime * blade.TurnSpeed);
        }

        private int IsAxisRotation(Axis axis) {
            return blade.Axis == axis ? 1 : 0;
        }
    }
}