using UnityEngine;

namespace Ludias.Combat.StateMachines.Player
{
    public class PlayerImpactState : PlayerBaseState
    {
        private readonly int ImpactHash = Animator.StringToHash("Impact");
        private const float TRANSITION_DURATION = 0.1f;
        private float duration = 1f;

        public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.GetAnimator().CrossFadeInFixedTime(ImpactHash, TRANSITION_DURATION);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);

            duration -= Time.deltaTime;

            if (duration <= 0f)
            {
                ReturnToLocomotion();
            }
        }

        public override void Exit()
        {

        }
    }
}
