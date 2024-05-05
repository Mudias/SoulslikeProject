using UnityEngine;

namespace Ludias.StateMachines.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
        private readonly int TargetingForwardSpeedHash = Animator.StringToHash("TargetingForwardSpeed");
        private readonly int TargetingRightSpeedHash = Animator.StringToHash("TargetingRightSpeed");

        public override void Enter()
        {
            stateMachine.OnTargetCanceled += StateMachine_OnTargetCanceled;

            stateMachine.GetAnimator().Play(TargetingBlendTreeHash);
        }

        public override void Tick(float deltaTime)
        {
            Vector3 movement = CalculateMovement();
            Move(movement, stateMachine.GetMoveSpeed(), deltaTime);

            UpdateAnimator(deltaTime);

            FaceTarget();
        }

        public override void Exit()
        {
            stateMachine.OnTargetCanceled -= StateMachine_OnTargetCanceled;
        }

        private void StateMachine_OnTargetCanceled(object sender, System.EventArgs e)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

        private Vector3 CalculateMovement()
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
