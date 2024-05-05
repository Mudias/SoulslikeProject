using Ludias.StateMachines.Player;
using Unity.Cinemachine;
using UnityEngine;

namespace Ludias.Characters
{
    public class EnemyTargeter : MonoBehaviour
    {
        [SerializeField] CinemachineCamera lockOnTargetCamera;
        [SerializeField] Transform enemyToTarget;

        PlayerStateMachine playerStateMachine;
        GameObject playerGO;

        private void Start()
        {
            playerGO = GameObject.FindGameObjectWithTag("Player");
            playerStateMachine = playerGO.GetComponent<PlayerStateMachine>();

            playerStateMachine.OnEnemyTargeted += PlayerStateMachine_OnEnemyTargeted;
            playerStateMachine.OnTargetCanceled += PlayerStateMachine_OnTargetCanceled;
        }

        public void ActivateLockOnCamera()
        {
            lockOnTargetCamera.gameObject.SetActive(true);
        }

        public void DeactivateLockOnCamera()
        {
            lockOnTargetCamera.gameObject.SetActive(false);
        }

        private void PlayerStateMachine_OnEnemyTargeted(object sender, System.EventArgs e)
        {
            ActivateLockOnCamera();
        }

        private void PlayerStateMachine_OnTargetCanceled(object sender, System.EventArgs e)
        {
            DeactivateLockOnCamera();
        }
    }
}