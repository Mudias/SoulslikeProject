using UnityEngine;

namespace Ludias.Combat.StateMachines
{
    public class EnemyIdleState : EnemyBaseState
    {
        public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }

        private readonly int LocomotionBlendTreeHash = Animator.StringToHash("LocomotionBlendTree");
        private readonly int SpeedHash = Animator.StringToHash("Speed");
        private const float CROSS_FADE_DURATION = 0.1f;
        private const float ANIMATOR_DAMP_TIME = 0.1f;

        public override void Enter()
        {
            stateMachine.GetAnimator().CrossFadeInFixedTime(LocomotionBlendTreeHash, CROSS_FADE_DURATION);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);

            if (IsInChaseRange())
            {
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
                return;
            }

            stateMachine.GetAnimator().SetFloat(SpeedHash, 0, ANIMATOR_DAMP_TIME, deltaTime);
        }

        public override void Exit()
        {

        }
    }
}
