using Ludias.Combat.StateMachines.Player;
using System;
using UnityEngine;
using UnityEngine.Windows;

namespace Ludias.Combat
{
    public class PlayerFreeLookState : PlayerBaseState
    {
        public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
        private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

        private const float ANIMATOR_DAMP_TIME = 0.1f;
        private const float CROSS_FADE_DURATION = 0.1f;
        private Vector3 moveDir;
        private Vector3 calculatedMoveDir;

        public override void Enter()
        {
            stateMachine.OnJumped += OnJump;
            stateMachine.OnEnemyTargeted += StateMachine_OnEnemyTargeted;

            stateMachine.GetAnimator().CrossFadeInFixedTime(FreeLookBlendTreeHash, CROSS_FADE_DURATION);
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            if (stateMachine.IsAttacking())
            {
                stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
                return;
            }

            calculatedMoveDir = CalculateMovement(deltaTime);

            Move(calculatedMoveDir, stateMachine.GetMoveSpeed(), deltaTime);

            if (stateMachine.GetMovementInputValue() == Vector2.zero)
            {
                stateMachine.GetAnimator().SetFloat(FreeLookSpeedHash, 0, ANIMATOR_DAMP_TIME, deltaTime);
                return;
            }

            stateMachine.GetAnimator().SetFloat(FreeLookSpeedHash, 1, ANIMATOR_DAMP_TIME, deltaTime);
            FaceMovementDirection(calculatedMoveDir, deltaTime);
        }

        public override void Exit()
        {
            stateMachine.OnJumped -= OnJump;
            stateMachine.OnEnemyTargeted -= StateMachine_OnEnemyTargeted;
        }

        public void OnJump(object sender, EventArgs e)
        {
            stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
        }

        private void StateMachine_OnEnemyTargeted(object sender, System.EventArgs e)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }

        private Vector3 CalculateMovement(float deltaTime)
        {
            moveDir = stateMachine.GetMovementInputValue().x * stateMachine.GetMainCam().transform.TransformDirection(Vector3.right) + stateMachine.GetMovementInputValue().y * stateMachine.GetMainCam().transform.TransformDirection(Vector3.forward);
            moveDir.y = 0;
            moveDir.Normalize();

            return moveDir;
        }

        private void FaceMovementDirection(Vector3 movement, float deltaTime)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), stateMachine.GetRotationDamping() * deltaTime);
        }
    }
}
