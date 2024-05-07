using Ludias.Combat.StateMachines.Player;
using UnityEditorInternal;
using UnityEngine;

namespace Ludias.Combat.StateMachines.Enemy
{
    public class EnemyImpactState : EnemyBaseState
    {
        private readonly int ImpactHash = Animator.StringToHash("Impact");
        private const float TRANSITION_DURATION = 0.1f;
        private float duration = 1f;

        public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.GetAnimator().CrossFadeInFixedTime(ImpactHash, TRANSITION_DURATION);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);

            duration -= deltaTime;

            if (duration <= 0f)
            {
                stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            }
        }

        public override void Exit()
        {

        }
    }
}
