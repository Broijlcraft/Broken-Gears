namespace BrokenGears.Enemies {
    using UnityEngine;

    public class TestEnemy : AEnemy {
        [SerializeField] private float defaultHealth;
        [SerializeField] private HealthEvent events;

        protected override float DefaultHealth() => defaultHealth;
        public override HealthEvent Events() => events;
    }
}