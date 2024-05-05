using Ludias.StateMachines.Player;
using System;
using UnityEngine;
using UnityEngine.Windows;

namespace Ludias
{
    public class PlayerFreeLookState : PlayerBaseState
    {
        public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

        private const float animatorDampTime = 0.1f;
        private const float gravity = -9.81f;
        private bool isGrounded;
        private Vector3 moveDir;
        private Vector3 calculatedMoveDir;
        private Vector3 playerVelocity;

        public override void Enter()
        {
            stateMachine.OnJumped += OnJump;
            stateMachine.OnEnemyTargeted += StateMachine_OnEnemyTargeted;
        }

        public override void Tick(float deltaTime)
        {
            isGrounded = stateMachine.GetCharacterController().isGrounded;

            calculatedMoveDir = CalculateMovement(deltaTime);
            if (stateMachine.GetMovementInputValue() == Vector2.zero)
            {
                stateMachine.GetAnimator().SetFloat(FreeLookSpeedHash, 0, animatorDampTime, deltaTime);
                return;
            }

            stateMachine.GetAnimator().SetFloat(FreeLookSpeedHash, 1, animatorDampTime, deltaTime);
            FaceMovementDirection(calculatedMoveDir, deltaTime);
        }

        public override void Exit()
        {
            stateMachine.OnJumped -= OnJump;
            stateMachine.OnEnemyTargeted -= StateMachine_OnEnemyTargeted;
        }

        public void OnJump(object sender, EventArgs e)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
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

            stateMachine.GetCharacterController().Move(moveDir * stateMachine.GetMoveSpeed() * deltaTime);
            playerVelocity.y += gravity * deltaTime;

            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2;
            }

            stateMachine.GetCharacterController().Move(playerVelocity * deltaTime);

            return moveDir;
        }

        private void FaceMovementDirection(Vector3 movement, float deltaTime)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), stateMachine.GetRotationDamping() * deltaTime);
        }
    }
}
