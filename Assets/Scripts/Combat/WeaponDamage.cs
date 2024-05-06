using System.Collections.Generic;
using UnityEngine;

namespace Ludias.Combat
{
	public class WeaponDamage : MonoBehaviour
	{
        [SerializeField] Collider myCollider;
        [SerializeField] int damageAmount;

        private List<Collider> collidedCollidersList = new();

        private void OnEnable()
        {
            collidedCollidersList.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == myCollider) return;

            if (collidedCollidersList.Contains(other)) return;

            collidedCollidersList.Add(other);

            if (other.TryGetComponent(out HealthSystem healthSystem))
            {
                healthSystem.TakeDamage(damageAmount);
            }
        }

        public void SetAttack(int damageAmount)
        {
            this.damageAmount = damageAmount;
        }
    }
}