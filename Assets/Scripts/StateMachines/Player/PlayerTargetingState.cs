using UnityEngine;

namespace Ludias.StateMachines.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.OnTargetCanceled += StateMachine_OnTargetCanceled;
        }

        public override void Tick(float deltaTime)
        {

        }

        public override void Exit()
        {
            stateMachine.OnTargetCanceled -= StateMachine_OnTargetCanceled;
        }

        private void StateMachine_OnTargetCanceled(object sender, System.EventArgs e)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

    }
}
