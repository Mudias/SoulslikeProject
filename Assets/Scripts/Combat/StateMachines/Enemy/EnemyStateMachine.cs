using Ludias.Combat.StateMachines.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Ludias.Combat.StateMachines.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        [SerializeField] Animator animator;
        [SerializeField] float playerChasingRange;
        [SerializeField] float moveSpeed;
        [SerializeField] float attackRange;
        [SerializeField] int attackDamage;
        [SerializeField] float attackKnockback;
        [SerializeField] WeaponDamage[] weaponDamageArray;

        private CharacterController characterController;
        private ForceReciever forceReciever;
        private NavMeshAgent agent;
        private HealthSystem healthSystem;

        private GameObject playerGO;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            forceReciever = GetComponent<ForceReciever>();
            healthSystem = GetComponent<HealthSystem>();
        }

        private void Start()
        {
            playerGO = GameObject.FindGameObjectWithTag("Player");

            agent = GetComponent<NavMeshAgent>();

            agent.updatePosition = false;
            agent.updateRotation = false;

            SwitchState(new EnemyIdleState(this));
        }

        private void OnEnable()
        {
            healthSystem.OnTakeDamage += HealthSystem_OnTakeDamage;
        }

        private void OnDisable()
        {
            healthSystem.OnTakeDamage -= HealthSystem_OnTakeDamage;
        }

        private void HealthSystem_OnTakeDamage(object sender, System.EventArgs e)
        {
            SwitchState(new EnemyImpactState(this));
        }

        public float GetMoveSpeed() => moveSpeed;
        public GameObject GetPlayerGO() => playerGO;
        public Animator GetAnimator() => animator;
        public float GetPlayerChasingRange() => playerChasingRange;
        public float GetAttackRange() => attackRange;
        public int GetAttackDamage() => attackDamage;
        public float GetAttackKnockback() => attackKnockback;
        public Transform GetPlayerTransform() => playerGO.transform;
        public CharacterController GetCharacterController() => characterController;
        public ForceReciever GetForceReciever() => forceReciever;
        public NavMeshAgent GetAgent() => agent;
        public WeaponDamage[] GetWeaponDamageArray() => weaponDamageArray;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, playerChasingRange);
        }
    }
}
