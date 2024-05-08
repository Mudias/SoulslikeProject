using System;
using UnityEngine;

namespace Ludias.Combat.StateMachines.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
        private readonly int TargetingForwardSpeedHash = Animator.StringToHash("TargetingForwardSpeed");
        private readonly int TargetingRightSpeedHash = Animator.StringToHash("TargetingRightSpeed");
        private const float CROSS_FADE_DURATION = 0.1f;

        public override void Enter()
        {
            stateMachine.OnJumped += OnJump;
            stateMachine.OnTargetCanceled += StateMachine_OnTargetCanceled;

            stateMachine.GetAnimator().CrossFadeInFixedTime(TargetingBlendTreeHash, CROSS_FADE_DURATION);
        }

        public override void Tick(float deltaTime)
        {
            if (stateMachine.IsAttacking())
            {
                stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
                return;
            }

            Vector3 movement = CalculateMovement(deltaTime);
            Move(movement, stateMachine.GetMoveSpeed(), deltaTime);

            UpdateAnimator(deltaTime);

            FaceTarget();
        }

        public override void Exit()
        {
            stateMachine.OnJumped -= OnJump;
            stateMachine.OnTargetCanceled -= StateMachine_OnTargetCanceled;
        }

        public void OnJump(object sender, EventArgs e)
        {
            stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
        }

        private void StateMachine_OnTargetCanceled(object sender, System.EventArgs e)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

        private Vector3 CalculateMovement(float deltaTime)
        {
            Vector3 movement = new Vector3();

            movement += stateMachine.transform.right * stateMachine.GetMovementInputValue().x;
            movement += stateMachine.transform.forward * stateMachine.GetMovementInputValue().y;

            return movement;
        }

        private void UpdateAnimator(float deltaTime)
        {
            if (stateMachine.GetMovementInputValue().y == 0)
            {
                stateMachine.GetAnimator().SetFloat(TargetingForwardSpeedHash, 0, 0.1f, deltaTime);
            }
            else
            {
                float value = stateMachine.GetMovementInputValue().y > 0 ? 1 : -1;
                stateMachine.GetAnimator().SetFloat(TargetingForwardSpeedHash, value, 0.1f, deltaTime);
            }
            if (stateMachine.GetMovementInputValue().x == 0)
            {
                stateMachine.GetAnimator().SetFloat(TargetingRightSpeedHash, 0, 0.1f, deltaTime);
            }
            else
            {
                float value = stateMachine.GetMovementInputValue().x > 0 ? 1 : -1;
                stateMachine.GetAnimator().SetFloat(TargetingRightSpeedHash, value, 0.1f, deltaTime);
            }
        }
    }
}
