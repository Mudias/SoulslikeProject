using UnityEngine;

namespace Ludias.Combat.StateMachines.Player
{
    public abstract class PlayerBaseState : State
    {
        protected PlayerStateMachine stateMachine;
        private const float gravity = -9.81f;
        private bool isGrounded;
        private Vector3 playerVelocity;

        public PlayerBaseState(PlayerStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void Tick(float deltaTime)
        {
            isGrounded = stateMachine.GetCharacterController().isGrounded;
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

        protected void FaceTarget()
        {
            Vector3 targetDirection = (stateMachine.GetEnemyTransform().position - stateMachine.transform.position).normalized;
            targetDirection.y = 0;

            stateMachine.transform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }
}
