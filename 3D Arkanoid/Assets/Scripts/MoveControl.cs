using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Arkanoid
{
    public class MoveControl : MonoBehaviour
    {
        #region
        private MoveContrl  _playerMoves;
        private PlayerSide _playerSide;
        private Vector2 _moveInput;
        private Rigidbody _playerRigidBody;
        #endregion


        private void Awake()
        {
            _playerMoves = new MoveContrl();
            if (GetComponent<GetPlayerSide>() != null)
            {
                _playerSide = GetComponent<GetPlayerSide>().GetNumber;
            }

        }

        private void Start()
        {
            
            _playerRigidBody = gameObject.GetComponent<Rigidbody>();
        }

        //start Player control on enable
        private void OnEnable()
        {
            _playerMoves.PlayerController.Enable();
        }

        private void FixedUpdate()
        {

            if (_playerSide == PlayerSide.FirstPlayer)
            {
                
                _moveInput = _playerMoves.PlayerController.PlayerFirstMove.ReadValue<Vector2>();
                Move(_playerRigidBody);
            }
            else if (_playerSide == PlayerSide.SecondPlayer)
            {
                _moveInput = _playerMoves.PlayerController.PlayerSecondMove.ReadValue<Vector2>();
                Move(_playerRigidBody);
            }
          
        }

        private void OnDisable()
        {
            _playerMoves.PlayerController.Disable();
        }

        //Players move control
        private void Move(Rigidbody player)
        {
            player.AddForce(_moveInput.x * GameManager.Manager.playerSpeed * transform.right);
            player.AddForce(_moveInput.y * GameManager.Manager.playerSpeed * transform.up);
        }
    }
}