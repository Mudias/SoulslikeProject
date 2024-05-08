using UnityEngine;

namespace Ludias.Combat.StateMachines.Player
{
    public class PlayerDodgingState : PlayerBaseState
    {
        private readonly int DodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
        private readonly int DodgeForwardHash = Animator.StringToHash("DodgeForward");
        private readonly int DodgeRightHash = Animator.StringToHash("DodgeRight");
        private const float CROSS_FADE_DURATION = 0.1f;

        private float remainingDodgeTime;
        private Vector3 dodgingDirectionInput;

        public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine)
        {
            this.dodgingDirectionInput = dodgingDirectionInput;
        }

        public override void Enter()
        {
            remainingDodgeTime = stateMachine.GetDodgeDuration();

            stateMachine.GetAnimator().SetFloat(DodgeForwardHash, dodgingDirectionInput.y);
            stateMachine.GetAnimator().SetFloat(DodgeRightHash, dodgingDirectionInput.x);
            stateMachine.GetAnimator().CrossFadeInFixedTime(DodgeBlendTreeHash, CROSS_FADE_DURATION);

            stateMachine.GetHealthSystem().SetInvulnerable(true);
        }

        public override void Tick(float deltaTime)
        {
            Vector3 movement = new Vector3();

            movement += stateMachine.transform.right * stateMachine.GetMovementInputValue().x * stateMachine.GetDodgeLength() / stateMachine.GetDodgeDuration();
            movement += stateMachine.transform.forward * stateMachine.GetMovementInputValue().y * stateMachine.GetDodgeLength() / stateMachine.GetDodgeDuration();

            Move(movement, 1, deltaTime);

            FaceTarget();

            remainingDodgeTime -= deltaTime;

            if (remainingDodgeTime <= 0)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
        }

        public override void Exit()
        {
            stateMachine.GetHealthSystem().SetInvulnerable(false);
        }
    }
}
