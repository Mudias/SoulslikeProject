using UnityEditorInternal;
using UnityEngine;

namespace Ludias.Combat.StateMachines
{
    public class EnemyChasingState : EnemyBaseState
    {
        public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

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
            if (!IsInChaseRange())
            {
                stateMachine.SwitchState(new EnemyIdleState(stateMachine));
                return;
            }

            MoveToPlayer(deltaTime);

            stateMachine.GetAnimator().SetFloat(SpeedHash, 1, ANIMATOR_DAMP_TIME, deltaTime);
        }

        public override void Exit()
        {
            stateMachine.GetAgent().ResetPath();
            stateMachine.GetAgent().velocity = Vector3.zero;
        }

        private void MoveToPlayer(float deltaTime)
        {
            stateMachine.GetAgent().destination = stateMachine.GetPlayerGO().transform.position;
            Move(stateMachine.GetAgent().desiredVelocity.normalized, stateMachine.GetMoveSpeed(), deltaTime);

            stateMachine.GetAgent().velocity = stateMachine.GetCharacterController().velocity;
        }
    }
}
