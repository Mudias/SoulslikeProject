using System;
using UnityEngine;

namespace Ludias.Combat
{
    public class HealthSystem : MonoBehaviour
    {
        public event EventHandler OnTakeDamage;

        [SerializeField] int maxHealth = 100;

        private int currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damageAmount)
        {
            if (currentHealth == 0) return;

            currentHealth = Mathf.Max(currentHealth - damageAmount, 0);

            OnTakeDamage?.Invoke(this, EventArgs.Empty);

            Debug.Log($"Took {damageAmount} damage, current health: {currentHealth}");

            if (currentHealth == 0)
            {
                //Die();
                Debug.Log($"{gameObject.name} died");
            }
        }
    }
}
