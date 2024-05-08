using System;
using UnityEditorInternal;
using UnityEngine;

namespace Ludias.Combat.StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        public event EventHandler OnJumped;
        public event EventHandler OnEnemyTargeted;
        public event EventHandler OnTargetCanceled;

        [SerializeField] float moveSpeed;
        [SerializeField] float rotationDamping;
        [SerializeField] float dodgeDuration;
        [SerializeField] float dodgeLength;
        [SerializeField] float jumpForce;
        [SerializeField] Animator animator;
        [SerializeField] Attack[] attacksArray;
        [SerializeField] WeaponDamage weaponDamage;
        
        private float previousDodgeTime = Mathf.NegativeInfinity;
        private float remainingDodgeTime;
        private bool isAttacking;
        private bool isTargetingEnemy;
        private Transform enemyTransform;
        private ForceReciever forceReciever;
        private Camera mainCam;
        private Vector2 movementInputValue;
        private CharacterController characterController;
        private HealthSystem healthSystem;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            forceReciever = GetComponent<ForceReciever>();
            healthSystem = GetComponent<HealthSystem>();
        }

        private void Start()
        {
            mainCam = Camera.main;
            enemyTransform = GameObject.FindGameObjectWithTag("Enemy").transform;

            SwitchState(new PlayerFreeLookState(this));
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

        private void HealthSystem_OnTakeDamage(object sender, EventArgs e)
        {
            SwitchState(new PlayerImpactState(this));
        }

        private void HealthSystem_OnDie(object sender, EventArgs e)
        {
            SwitchState(new PlayerDeadState(this));
        }

        public Camera GetMainCam() => mainCam;

        public void MovementInputValue(Vector2 input)
        {
            movementInputValue = input;
        }

        public Vector2 GetMovementInputValue() => movementInputValue;

        public void OnJump()
        {
            OnJumped?.Invoke(this, EventArgs.Empty);
        }

        public void Dodge()
        {
            if (movementInputValue == Vector2.zero) return;

            SwitchState(new PlayerDodgingState(this, movementInputValue));
        }

        public void OnTargetEnemy()
        {
            isTargetingEnemy = true;
            OnEnemyTargeted?.Invoke(this, EventArgs.Empty);
        }

        public void SetIsAttacking(bool attackState)
        {
             isAttacking = attackState;
        }

        public bool IsAttacking()
        {
            return isAttacking;
        }

        public void OnCancelTarget()
        {
            isTargetingEnemy = false;
            OnTargetCanceled?.Invoke(this, EventArgs.Empty);
        }

        public float GetMoveSpeed() => moveSpeed;
        public float GetRotationDamping() => rotationDamping;
        public float GetDodgeDuration() => dodgeDuration;
        public float GetPreviousDodgeTime() => previousDodgeTime;
        public float GetRemainingDodgeTime() => remainingDodgeTime;
        public float SetRemainingDodgeTime(float remainingDodgeTime) => this.remainingDodgeTime = remainingDodgeTime;
        public void DecrementRemainingDodgeTime(float deltaTime) => remainingDodgeTime -= deltaTime;
        public float GetDodgeLength() => dodgeLength;
        public bool IsTargetingEnemy() => isTargetingEnemy;
        public HealthSystem GetHealthSystem() => healthSystem;
        public WeaponDamage GetWeaponDamage() => weaponDamage;
        public Attack[] GetAttacksArray() => attacksArray;

        public ForceReciever GetForceReciever() => forceReciever;
        public Transform GetEnemyTransform() => enemyTransform;

        public CharacterController GetCharacterController() => characterController;

        public Animator GetAnimator() => animator;
    }
}
