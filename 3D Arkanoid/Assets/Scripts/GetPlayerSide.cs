using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class GetPlayerSide : MonoBehaviour
    {
        [Tooltip("Player side"), SerializeField]
        private PlayerSide _playerNumber;

        public PlayerSide GetNumber => _playerNumber;
    }
}