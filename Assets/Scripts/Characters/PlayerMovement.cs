using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ludias.Characters
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float moveSpeed;

        private bool isGrounded;
        private float gravity = -9.81f;
        private Camera mainCam;
        private CharacterController characterController;
        private Vector3 moveDir;
        private Vector3 playerVelocity;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            mainCam = Camera.main;
        }

        private void Update()
        {
            isGrounded = characterController.isGrounded;
        }

        public void ProcessMove(Vector2 input)
        {
            moveDir = input.x * mainCam.transform.TransformDirection(Vector3.right) + input.y * mainCam.transform.TransformDirection(Vector3.forward);
            moveDir.y = 0;
            moveDir.Normalize();

            characterController.Move(moveDir * moveSpeed * Time.deltaTime);
            return;
            playerVelocity.y += gravity * Time.deltaTime;

            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2;
            }

            characterController.Move(playerVelocity * Time.deltaTime);
        }
    }
}
