using System.Collections.Generic;
using UnityEngine;

namespace Ludias.Combat
{
	public class WeaponDamage : MonoBehaviour
	{
        [SerializeField] Collider myCollider;

        private int damageAmount;
        private float knockback;
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

        public void SetAttack(int damageAmount, float knockback)
        {
            this.damageAmount = damageAmount;
            this.knockback = knockback;
        }
    }
}