using System;
using UnityEngine;

namespace Ludias.Combat.StateMachines.Player
{
    [Serializable]
    public class Attack
    {
        [SerializeField] string animationName;
        [SerializeField] float transitionDuration;
        [SerializeField] int comboStateIndex = -1;
        [SerializeField] float comboAttackTime;
        [SerializeField] float forceTime;
        [SerializeField] float force;
        [SerializeField] int damageAmount;

        public int GetDamageAmount() => damageAmount;
        public string GetAnimationName() => animationName;
        public float GetTransitionDuration() => transitionDuration;
        public int GetComboStateIndex() => comboStateIndex;
        public float GetComboAttackTime() => comboAttackTime;
        public float GetForceTime() => forceTime;
        public float GetForce() => force;
    }
}
