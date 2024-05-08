using UnityEngine;

namespace Ludias.Combat.StateMachines.Enemy
{
    public class EnemyDeadState : EnemyBaseState
    {
        public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            foreach (WeaponDamage weapon in stateMachine.GetWeaponDamageArray())
            {
                weapon.gameObject.SetActive(false);
            }
        }

        public override void Tick(float deltaTime)
        {

        }

        public override void Exit()
        {

        }
    }
}
