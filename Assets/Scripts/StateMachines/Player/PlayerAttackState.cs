using System;
using UnityEngine;

namespace Ludias.StateMachines.Player
{
    public class PlayerAttackState : PlayerBaseState
    {
        public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
        private float timer;

        public override void Enter()
        {
            stateMachine.OnJumped += OnJump;
        }

        public override void Tick(float deltaTime)
        {
            timer += Time.deltaTime;

            stateMachine.GetAnimator().SetFloat(FreeLookSpeedHash, 0, 0.1f, deltaTime);
            stateMachine.GetAnimator().SetFloat(FreeLookSpeedHash, 1, 0.1f, deltaTime);
        }

        public override void Exit()
        {
            stateMachine.OnJumped -= OnJump;
        }

        public void OnJump(object sender, EventArgs e)
        {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine));
        }

        //private Vector3 CalculateMovement()
    }
}
