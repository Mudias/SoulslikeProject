using Ludias.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ludias.Core
{
    public class InputManager : MonoBehaviour
    {
        PlayerInputActions playerInputActions;
        PlayerInputActions.PlayerActions playerAction;
        PlayerMovement playerMovement;
        GameObject playerGO;

        private void Awake()
        {
            playerAction = playerInputActions.Player;
            playerInputActions = new PlayerInputActions();
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
