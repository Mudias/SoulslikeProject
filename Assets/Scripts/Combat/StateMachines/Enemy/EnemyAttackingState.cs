using UnityEngine;

namespace Ludias.Combat.StateMachines.Enemy
{
    public class EnemyAttackingState : EnemyBaseState
    {
        private readonly int AttackHash = Animator.StringToHash("Attack");
        private const float TRANSITION_DURATION = 0.1f;

        public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            foreach (WeaponDamage weaponDamage in stateMachine.GetWeaponDamageArray())
            {
                weaponDamage.SetAttack(stateMachine.GetAttackDamage(), stateMachine.GetAttackKnockback());
            }

            stateMachine.GetAnimator().CrossFadeInFixedTime(AttackHash, TRANSITION_DURATION);
        }

        public override void Tick(float deltaTime)
        {
            if (GetNormalizedTime(stateMachine.GetAnimator()) >= 1)
            {
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            }
        }

        public override void Exit()
        {

        }
    }
}
