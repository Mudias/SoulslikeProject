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

        private HealthSystem playerHealthSystem;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            forceReciever = GetComponent<ForceReciever>();
            healthSystem = GetComponent<HealthSystem>();
        }

        private void Start()
        {
            playerHealthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();

            agent = GetComponent<NavMeshAgent>();

            agent.updatePosition = false;
            agent.updateRotation = false;

            SwitchState(new EnemyIdleState(this));
        }

        private void OnEnable()
        {
            healthSystem.OnTakeDamage += HealthSystem_OnTakeDamage;
            healthSystem.OnDie += HealthSystem_OnDie;
        }

        private void OnDisable()
        {
            healthSystem.OnTakeDamage -= HealthSystem_OnTakeDamage;
            healthSystem.OnDie -= HealthSystem_OnDie;
        }

        private void HealthSystem_OnTakeDamage(object sender, System.EventArgs e)
        {
            SwitchState(new EnemyImpactState(this));
        }

        private void HealthSystem_OnDie(object sender, System.EventArgs e)
        {
            SwitchState(new EnemyDeadState(this));
        }

        public float GetMoveSpeed() => moveSpeed;
        public HealthSystem GetPlayerHealthSystem() => playerHealthSystem;
        public Animator GetAnimator() => animator;
        public float GetPlayerChasingRange() => playerChasingRange;
        public float GetAttackRange() => attackRange;
        public int GetAttackDamage() => attackDamage;
        public float GetAttackKnockback() => attackKnockback;
        public Transform GetPlayerTransform() => playerHealthSystem.transform;
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
