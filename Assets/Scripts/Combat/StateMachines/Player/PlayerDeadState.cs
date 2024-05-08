using UnityEngine;

namespace Ludias.Combat.StateMachines.Player
{
    public class PlayerDeadState : PlayerBaseState
    {
        public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.GetWeaponDamage().gameObject.SetActive(false);
        }

        public override void Tick(float deltaTime)
        {

        }

        public override void Exit()
        {

        }
    }
}
