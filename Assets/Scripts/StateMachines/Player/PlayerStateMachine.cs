using System;
using UnityEngine;

namespace Ludias.StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        public event EventHandler OnJumped;

        [SerializeField] Animator animator;

        private CharacterController characterController;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            SwitchState(new PlayerAttackState(this));
        }

        public void OnJump()
        {
            OnJumped?.Invoke(this, EventArgs.Empty);
        }

        public Animator GetAnimator()
        {
            return animator;
        }
    }
}
