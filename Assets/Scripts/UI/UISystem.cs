using Ludias.Combat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ludias.UI
{
    public class UISystem : MonoBehaviour
    {
        [SerializeField] Image playerHealthbarImage;
        [SerializeField] Image enemyHealthbarImage;
        [SerializeField] TextMeshProUGUI playerHPLabel;
        [SerializeField] TextMeshProUGUI enemyHPLabel;

        HealthSystem playerHealthSystem;
        HealthSystem enemyHealthSystem;

        private void Start()
        {
            playerHealthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
            enemyHealthSystem = GameObject.FindGameObjectWithTag("Enemy").GetComponent<HealthSystem>();

            playerHealthSystem.OnTakeDamage += PlayerHealthSystem_OnTakeDamage;
            enemyHealthSystem.OnTakeDamage += EnemyHealthSystem_OnTakeDamage;
        }

        private void PlayerHealthSystem_OnTakeDamage(object sender, System.EventArgs e)
        {
            UpdatePlayerHealhbar();
        }

        private void EnemyHealthSystem_OnTakeDamage(object sender, System.EventArgs e)
        {
            UpdateEnemyHealthbar();
        }

        private void UpdatePlayerHealhbar()
        {
            playerHealthbarImage.fillAmount = playerHealthSystem.GetHealthNormalized();
            playerHPLabel.text = $"Player hp: {playerHealthSystem.GetCurrentHealth()}/{playerHealthSystem.GetMaxHealth()}";
        }

        private void UpdateEnemyHealthbar()
        {
            enemyHealthbarImage.fillAmount = enemyHealthSystem.GetHealthNormalized();
            enemyHPLabel.text = $"Enemy hp: {enemyHealthSystem.GetCurrentHealth()}/{enemyHealthSystem.GetMaxHealth()}";
        }
    }
}
