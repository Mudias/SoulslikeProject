using UnityEngine;

namespace Ludias.StateMachines.Player
{
    public abstract class PlayerBaseState : State
    {
        protected PlayerStateMachine stateMachine;

        public PlayerBaseState(PlayerStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        protected void Move(Vector3 motion, float moveSpeed, float deltaTime)
        {
            stateMachine.GetCharacterController().Move(motion * moveSpeed * deltaTime);
        }

        protected void FaceTarget()
        {
            Vector3 targetDirection = (stateMachine.GetEnemyTransform().position - stateMachine.transform.position).normalized;
            targetDirection.y = 0;

            stateMachine.transform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }
}
