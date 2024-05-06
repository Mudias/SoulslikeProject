using System;
using UnityEngine;

namespace Ludias.Combat.StateMachines.Player
{
    public class PlayerAttackingState : PlayerBaseState
    {
        private float previousFrameTime;
        private bool hasAlreadyAppliedForce;
        private Attack attack;

        public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
        {
            attack = stateMachine.GetAttacksArray()[attackIndex];
        }

        private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

        public override void Enter()
        {
            stateMachine.GetAnimator().CrossFadeInFixedTime(attack.GetAnimationName(), attack.GetTransitionDuration());
            //stateMachine.OnJumped += OnJump;
            stateMachine.GetWeaponDamage().SetAttack(attack.GetDamageAmount());
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            FaceTarget();

            float normalizedTime = GetNormalizedTime();

            if (normalizedTime < 1f)
            {
                if (normalizedTime >= attack.GetForceTime())
                {
                    TryApplyForce();
                }

                if (stateMachine.IsAttacking())
                {
                    TryComboAttack(normalizedTime);
                }
            }else
            {
                // go back to locomotion
                if (stateMachine.IsTargetingEnemy())
                {
                    stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
                }else
                {
                    stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                }
            }

            previousFrameTime = normalizedTime;
        }

        public override void Exit()
        {
            //stateMachine.OnJumped -= OnJump;
        }

        public void OnJump(object sender, EventArgs e)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
        }

        private void TryComboAttack(float normalizedTime)
        {
            if (attack.GetComboStateIndex() == -1) return;

            if (normalizedTime < attack.GetComboAttackTime()) return;

            stateMachine.SwitchState(new PlayerAttackingState(stateMachine,attack.GetComboStateIndex()));
        }

        private void TryApplyForce()
        {
            if (hasAlreadyAppliedForce) return;

            stateMachine.GetForceReciever().AddForce(stateMachine.transform.forward * attack.GetForce());

            hasAlreadyAppliedForce = true;
        }

        private float GetNormalizedTime()
        {
            AnimatorStateInfo currentStateInfo = stateMachine.GetAnimator().GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo nextStateInfo = stateMachine.GetAnimator().GetNextAnimatorStateInfo(0);

            if (stateMachine.GetAnimator().IsInTransition(0) && nextStateInfo.IsTag("Attack"))
            {
                return nextStateInfo.normalizedTime;
            }else if (!stateMachine.GetAnimator().IsInTransition(0) && currentStateInfo.IsTag("Attack"))
            {
                return currentStateInfo.normalizedTime;
            }else
            {
                return 0f;
            }
        }
    }
}
