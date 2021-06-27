
namespace BrokenGears.Old {
    public class FreezeFX : StatusFX {

        private void Update() {
            if (shouldFX) {
                enemyPathing.SetSpeed(freezeStrength / 100 * enemyPathing.GetDefaultSpeed());
                Timer();
            }
        }

        protected override void StopUsing() {
            shouldFX = false;
            enemyPathing.SetSpeed(enemyPathing.GetDefaultSpeed());
            base.StopUsing();
        }
    }
}