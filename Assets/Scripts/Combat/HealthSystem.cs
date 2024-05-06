using UnityEngine;

namespace Ludias.Combat
{
    public class HealthSystem : MonoBehaviour
    {
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

            Debug.Log($"Took {damageAmount} damage, current health: {currentHealth}");

            if (currentHealth == 0)
            {
                //Die();
                Debug.Log($"{gameObject.name} died");
            }
        }
    }
}
