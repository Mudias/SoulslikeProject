using Ludias.Combat.StateMachines.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Ludias.Combat.StateMachines
{
    public class EnemyStateMachine : StateMachine
    {
        [SerializeField] Animator animator;
        [SerializeField] float playerChasingRange;
        [SerializeField] float moveSpeed;

        private CharacterController characterController;
        private ForceReciever forceReciever;
        private NavMeshAgent agent;

        private GameObject playerGO;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            forceReciever = GetComponent<ForceReciever>();
        }

        private void Start()
        {
            playerGO = GameObject.FindGameObjectWithTag("Player");

            agent = GetComponent<NavMeshAgent>();

            agent.updatePosition = false;
            agent.updateRotation = false;

            SwitchState(new EnemyIdleState(this));
        }

        public float GetMoveSpeed() => moveSpeed;
        public GameObject GetPlayerGO() => playerGO;
        public Animator GetAnimator() => animator;
        public float GetPlayerChasingRange() => playerChasingRange;
        public CharacterController GetCharacterController() => characterController;
        public ForceReciever GetForceReciever() => forceReciever;
        public NavMeshAgent GetAgent() => agent;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, playerChasingRange);
        }
    }
}
