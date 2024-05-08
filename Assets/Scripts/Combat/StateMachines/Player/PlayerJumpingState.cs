using UnityEngine;

namespace Ludias.Combat.StateMachines.Player
{
    public class PlayerJumpingState : PlayerBaseState
    {
        private readonly int JumpHash = Animator.StringToHash("Jump");
        private const float CROSS_FADE_DURATION = 0.1f;

        public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.GetAnimator().CrossFadeInFixedTime(JumpHash, CROSS_FADE_DURATION);
        }

        public override void Tick(float deltaTime)
        {
            FaceTarget();
        }

        public override void Exit()
        {

        }
    }
}
