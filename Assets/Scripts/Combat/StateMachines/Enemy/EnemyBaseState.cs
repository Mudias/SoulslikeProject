using UnityEngine;

namespace Ludias.Combat.StateMachines
{
    public class EnemyBaseState : State
    {
        protected EnemyStateMachine stateMachine;

        private const float gravity = -9.81f;
        private bool isGrounded;
        private Vector3 playerVelocity;

        public EnemyBaseState(EnemyStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void Enter()
        {
            throw new System.NotImplementedException();
        }

        public override void Tick(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        protected void Move(Vector3 moveDir, float moveSpeed, float deltaTime)
        {
            playerVelocity.y += gravity * deltaTime;

            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2;
            }

            stateMachine.GetCharacterController().Move((moveDir + playerVelocity + stateMachine.GetForceReciever().MovementForce) * moveSpeed * deltaTime);
        }

        protected void Move(float deltaTime)
        {
            Move(Vector3.zero, 0, deltaTime);

        }

        protected bool IsInChaseRange()
        {
            float distanceToPlayerSqr = (stateMachine.GetPlayerGO().transform.position - stateMachine.transform.position).sqrMagnitude;

            return distanceToPlayerSqr <= stateMachine.GetPlayerChasingRange() * stateMachine.GetPlayerChasingRange();
        }
    }
}
