namespace BrokenGears.Combat {
    using UnityEngine;
    using UnityEngine.Events;

    public class RivetGun : AWeaponizedTurret {
        [SerializeField] private UnityEvent onAttack;
        public override UnityEvent OnAttack() => onAttack;
    }
}