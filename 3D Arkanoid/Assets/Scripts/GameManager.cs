using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] public GameObject ball;
        [SerializeField] public GameObject firstBoarder;
        [SerializeField] public GameObject secondBoarder;
        [SerializeField] public Transform firstBallHolder;
        [SerializeField] public Transform secondBallHolder;
        [SerializeField] public Transform blocksParent;

        [Header("Parameters Settings")]
        [Tooltip("Amount of lifes before game ended"), SerializeField] public int lifes = 3;
        [Tooltip("Players platform speed"), SerializeField] public float playerSpeed;
        [Tooltip("Starting ball speed"), SerializeField] public float ballSpeed;
        [Tooltip("Maximum of ball speed after bouncing"), SerializeField] public float maxBallSpeed;
        [Tooltip("Ball speed rise step after bouncing"), SerializeField] public float ballSpeedRise;
        [Tooltip("Blocks Quantity of each block type in the level"), SerializeField] public int quantity;

        public static GameManager Manager;

        private void Awake()
        {
            Manager = this;
        }
    }
}