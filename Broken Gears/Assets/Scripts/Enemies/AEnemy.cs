namespace BrokenGears.Enemies {
    using UnityEngine;
    using UnityEngine.Events;

    public abstract class AEnemy : MonoBehaviour {
        [SerializeField, ReadOnly] protected float currentHealth;
        protected abstract float DefaultHealth();
        public abstract HealthEvent Events(); 

        public bool IsAlive { get; private set; }

        private void Start() {
            if (Events() != null) {
                currentHealth = DefaultHealth();
                IsAlive = true;

                Events().OnDamage.AddListener(OnDamage_Internal);
                Events().OnDeath.AddListener(OnDeath_Internal);
            }
        }

        public void DoDamage(int amount) {
            if (IsAlive) {
                HealthArgs args = new HealthArgs(amount);
                Events()?.OnDamage?.Invoke(args);
            }
        }        

        public void DoDeath() {
            Events()?.OnDeath?.Invoke();
        }

        private void OnDamage_Internal(HealthArgs args) {
            currentHealth = Mathf.Clamp(currentHealth - args.amount, 0, DefaultHealth());

            if(currentHealth == 0) {
                DoDeath();
            }
        }

        private void OnDeath_Internal() {
            IsAlive = false;
            print("died");
        }

        [System.Serializable]
        public class HealthArgs : UnityEvent {
            public float amount;

            public HealthArgs(float amount) {
                this.amount = amount;
            }
        }

        [System.Serializable]
        public class HealthEvent {
            public UnityEvent OnDeath;
            public UnityEvent<HealthArgs> OnDamage;
        }
    }
}