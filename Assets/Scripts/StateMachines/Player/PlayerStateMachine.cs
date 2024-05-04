using System;
using UnityEngine;

namespace Ludias.StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        public event EventHandler OnJumped;

        private void Start()
        {
            SwitchState(new PlayerAttackState(this));
        }

        public void OnJump()
        {
            OnJumped?.Invoke(this, EventArgs.Empty);
        }
    }
}
