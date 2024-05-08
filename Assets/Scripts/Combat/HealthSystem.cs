using System;
using UnityEngine;

namespace Ludias.Combat
{
    public class HealthSystem : MonoBehaviour
    {
        public event EventHandler OnTakeDamage;
        public event EventHandler OnDie;

        [SerializeField] int maxHealth = 100;

        public bool IsDead => currentHealth == 0;
        private bool isInvulnerable;
        private int currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public bool IsInvulnerable() => isInvulnerable;

        public void SetInvulnerable(bool isInvulnerable)
        {
            this.isInvulnerable = isInvulnerable;
        }

        public void TakeDamage(int damageAmount)
        {
            if (currentHealth == 0) return;

            if (isInvulnerable) return;

            currentHealth = Mathf.Max(currentHealth - damageAmount, 0);

            OnTakeDamage?.Invoke(this, EventArgs.Empty);

            Debug.Log($"Took {damageAmount} damage, current health: {currentHealth}");

            if (currentHealth == 0)
            {
                //Die();
                Debug.Log($"{gameObject.name} died");

                OnDie?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
