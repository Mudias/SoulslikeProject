using Ludias.Combat.Characters;
using Ludias.Combat.StateMachines.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ludias.Core
{
    public class InputManager : MonoBehaviour
    {
        public PlayerInputActions playerInputActions;
        public PlayerInputActions.PlayerActions playerAction;

        private PlayerMovement playerMovement;
        private PlayerStateMachine playerStateMachine;
        private GameObject playerGO;

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerAction = playerInputActions.Player;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            playerGO = GameObject.FindGameObjectWithTag("Player");
            playerMovement = playerGO.GetComponent<PlayerMovement>();
            playerStateMachine = playerGO.GetComponent<PlayerStateMachine>();

            playerAction.Jump.performed += ctx => playerStateMachine.OnJump();
            playerAction.TargetEnemy.performed += ctx => playerStateMachine.OnTargetEnemy();
            playerAction.CancelTarget.performed += ctx => playerStateMachine.OnCancelTarget();
            playerAction.Attack.performed += ctx => playerStateMachine.SetIsAttacking(true);
            playerAction.Attack.canceled += ctx => playerStateMachine.SetIsAttacking(false);
            playerAction.Dodge.performed += ctx => playerStateMachine.Dodge();
        }

        private void OnEnable()
        {
            playerAction.Enable();
        }

        private void OnDisable()
        {
            playerAction.Disable();
        }

        private void Update()
        {
            playerStateMachine.MovementInputValue(playerAction.Move.ReadValue<Vector2>());
            //playerMovement.ProcessMove(playerAction.Move.ReadValue<Vector2>());
        }
    }
}
