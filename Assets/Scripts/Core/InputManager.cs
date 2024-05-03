using Ludias.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ludias.Core
{
    public class InputManager : MonoBehaviour
    {
        public PlayerInputActions playerInputActions;
        public PlayerInputActions.PlayerActions playerAction;
        PlayerMovement playerMovement;
        GameObject playerGO;

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerAction = playerInputActions.Player;
        }

        private void Start()
        {
            playerGO = GameObject.FindGameObjectWithTag("Player");
            playerMovement = playerGO.GetComponent<PlayerMovement>();
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
            playerMovement.ProcessMove(playerAction.Move.ReadValue<Vector2>());
        }
    }
}
