using UnityEngine;

namespace Ludias.Combat.StateMachines.Player
{
    public class PlayerFallingState : PlayerBaseState
    {
        private readonly int FallHash = Animator.StringToHash("Fall");
        private const float CROSS_FADE_DURATION = 0.1f;

        public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.GetAnimator().CrossFadeInFixedTime(FallHash, CROSS_FADE_DURATION);
        }

        public override void Tick(float deltaTime)
        {
            if (stateMachine.GetCharacterController().isGrounded)
            {
                ReturnToLocomotion();
            }

            FaceTarget();
        }

        public override void Exit()
        {

        }
    }
}
