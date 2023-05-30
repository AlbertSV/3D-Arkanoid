using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Arkanoid
{
    public class GameManager : MonoBehaviour
    {
        public int difficulty;
        //controlled by difficulty
        public static int lives = 5;
        public static float ballSpeed = 75f;

        [SerializeField] public GameObject ball;
        [SerializeField] public GameObject firstBoarder;
        [SerializeField] public GameObject secondBoarder;
        [SerializeField] public Transform firstBallHolder;
        [SerializeField] public Transform secondBallHolder;
        [SerializeField] public Transform blocksParent;
        [SerializeField] public Canvas canvasGame;
        [SerializeField] public Transform heartHolderFirst;
        [SerializeField] public Transform heartHolderSecond;
        [SerializeField] public TMP_Text livesTextFirst;
        [SerializeField] public TMP_Text livesTextSecond;

        [Header("Parameters Settings")]
        [Tooltip("Players platform speed"), SerializeField] public float playerSpeed;
        [Tooltip("Maximum of ball speed after bouncing"), SerializeField] public float maxBallSpeed;
        [Tooltip("Ball speed rise step after bouncing"), SerializeField] public float ballSpeedRise;
        [Tooltip("Blocks Quantity of each block type in the level"), SerializeField] public int quantity;
        [Tooltip("The amount on how much speed will grow with each difficulty level"), SerializeField] private float ballSpeedIncrease = 25f;
        [Tooltip("The amount on how much lives will down with each difficulty level"), SerializeField] private int livesDecrease = 2;


        public static GameManager Manager;

        private void Awake()
        {
            Manager = this;
            difficulty = PlayerPrefs.GetInt("difficulty");
        }
        private void Start()
        {
            
        }
        private void Update()
        {
            GameDifficulty(difficulty);
        }
        public void GameDifficulty(int difficulty)
        {
            if (difficulty == 0)
            {
                ballSpeed = 75f;
                lives = 5;
            }
            else if (difficulty == 1)
            {
                ballSpeed += ballSpeedIncrease;
                lives -= livesDecrease;
            }
            else
            {
                ballSpeed += ballSpeedIncrease * 2;
                lives -= livesDecrease * 2;
            }
        }
    }
}