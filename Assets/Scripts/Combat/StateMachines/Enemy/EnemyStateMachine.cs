using UnityEngine;

namespace Ludias.Combat.StateMachines
{
    public class EnemyStateMachine : StateMachine
    {
        [SerializeField] Animator animator;
        [SerializeField] float playerChasingRange;

        private GameObject playerGO;

        private void Start()
        {
            playerGO = GameObject.FindGameObjectWithTag("Player");

            SwitchState(new EnemyIdleState(this));
        }

        public GameObject GetPlayerGO() => playerGO;
        public Animator GetAnimator() => animator;
        public float GetPlayerChasingRange => playerChasingRange;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, playerChasingRange);
        }
    }
}
