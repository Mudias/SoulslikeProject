using System;
using UnityEngine;

namespace Ludias.StateMachines
{
    [Serializable]
    public class Attack
    {
        [SerializeField] string animationName;
        [SerializeField] float transitionDuration;
        [SerializeField] int comboStateIndex = -1;
        [SerializeField] float comboAttackTime;

        public string GetAnimationName() => animationName;
        public float GetTransitionDuration() => transitionDuration;
        public int GetComboStateIndex() => comboStateIndex;
        public float GetComboAttackTime() => comboAttackTime;
    }
}
