using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Arkanoid
{
    public class MoveControl : MonoBehaviour
    {
        #region
        private MoveContrl playerMoves;
        private PlayerSide playerSide;
        private Vector2 moveInput;
        private Rigidbody playerRigidBody;
        #endregion


        private void Awake()
        {
            playerMoves = new MoveContrl();

            if (GetComponent<GetPlayerSide>() != null)
            {
                playerSide = GetComponent<GetPlayerSide>().GetNumber;
            }

        }

        private void Start()
        {
            playerRigidBody = gameObject.GetComponent<Rigidbody>();
        }

        //start Player control on enable
        private void OnEnable()
        {
            playerMoves.PlayerController.Enable();
        }

        private void FixedUpdate()
        {

            if (playerSide == PlayerSide.FirstPlayer)
            {
                
                moveInput = playerMoves.PlayerController.PlayerFirstMove.ReadValue<Vector2>();
                Move(playerRigidBody);
            }
            else if (playerSide == PlayerSide.SecondPlayer)
            {
                moveInput = playerMoves.PlayerController.PlayerSecondMove.ReadValue<Vector2>();
                Move(playerRigidBody);
            }
          
        }

        private void OnDisable()
        {
            playerMoves.PlayerController.Disable();
        }

        //Players move control
        private void Move(Rigidbody player)
        {
            player.AddForce(moveInput.x * GameManager.Manager.playerSpeed * transform.right);
            player.AddForce(moveInput.y * GameManager.Manager.playerSpeed * transform.up);
        }
    }
}