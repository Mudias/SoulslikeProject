using System;
using UnityEngine;

namespace Ludias.StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        public event EventHandler OnJumped;
        public event EventHandler OnEnemyTargeted;
        public event EventHandler OnTargetCanceled;

        [SerializeField] float moveSpeed;
        [SerializeField] Animator animator;
        [SerializeField] float rotationDamping;

        private Transform enemyTransform;
        private Camera mainCam;
        private Vector2 movementInputValue;
        private CharacterController characterController;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            mainCam = Camera.main;
            enemyTransform = GameObject.FindGameObjectWithTag("Enemy").transform;

            SwitchState(new PlayerFreeLookState(this));
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

        public void OnTargetEnemy()
        {
            OnEnemyTargeted?.Invoke(this, EventArgs.Empty);
        }

        public void OnCancelTarget()
        {
            OnTargetCanceled?.Invoke(this, EventArgs.Empty);
        }

        public float GetMoveSpeed() => moveSpeed;
        public float GetRotationDamping() => rotationDamping;

        public Transform GetEnemyTransform() => enemyTransform;

        public CharacterController GetCharacterController() => characterController;

        public Animator GetAnimator() => animator;
    }
}
