using UnityEngine;

namespace Ludias.Combat.StateMachines.Enemy
{
    public class EnemyAttackingState : EnemyBaseState
    {
        private readonly int AttackHash = Animator.StringToHash("Attack");
        private readonly int WhirlwindHash = Animator.StringToHash("Whirlwind");
        private const float TRANSITION_DURATION = 0.1f;

        private int[] attacksArray;

        public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            attacksArray = new int[] { AttackHash, WhirlwindHash};

            foreach (WeaponDamage weaponDamage in stateMachine.GetWeaponDamageArray())
            {
                weaponDamage.SetAttack(stateMachine.GetAttackDamage(), stateMachine.GetAttackKnockback());
            }

            int randomAttackIndex = Random.Range(0, attacksArray.Length);

            stateMachine.GetAnimator().CrossFadeInFixedTime(attacksArray[randomAttackIndex], TRANSITION_DURATION);
        }

        public override void Tick(float deltaTime)
        {
            if (GetNormalizedTime(stateMachine.GetAnimator()) >= 1)
            {
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            }

            FacePlayer();
        }

        public override void Exit()
        {

        }
    }
}
