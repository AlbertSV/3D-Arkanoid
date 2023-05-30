using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Arkanoid
{
    public class GameControl : MonoBehaviour
    {
        #region
        private MoveControl moveControl;
        private BallControl ballControl;
        private List<GameObject> blocks;
        private TriggeredControl triggerControl;
        private List<GameObject> blocksTypes;

        private Vector3 playerOneStartPos;
        private Vector3 playerTwoStartPos;
        private float nextLevelStep = -20f;
        private Vector3 levelCenter = new Vector3(0f, 7.5f, 0f);
        private float stepFromCenter = 1.5f;
        private float stepFromCenterLength = 3.5f;
        private float heartStepCanvas = -15f;
        private GameObject nextLevel;
        private GameObject ball;
        private Transform firstBallHolder;
        private Transform secondBallHolder;
        private GameObject firstBoarder;
        private GameObject secondBoarder;
        private GameObject heartPrefab;

        private List<GameObject> heartListFirst;
        private List<GameObject> heartListSecond;
        private int lives;
        #endregion

        private void Awake()
        {
            heartPrefab = Resources.Load<GameObject>("Prefabs/Heart");
            heartListFirst = new List<GameObject>();
            heartListSecond = new List<GameObject>();

            blocksTypes = new List<GameObject>
            {
                Resources.Load<GameObject>("Prefabs/Block Type1"),
                Resources.Load<GameObject>("Prefabs/Block Type2"),
                Resources.Load<GameObject>("Prefabs/Block Type3"),
                Resources.Load<GameObject>("Prefabs/Block Type4"),
            };
            blocks = new List<GameObject>();

            nextLevel = FindObjectOfType<LevelTwo>().gameObject;
            nextLevel.SetActive(false);

        }
        private void Start()
        {
            ball = GameManager.Manager.ball;
            firstBallHolder = GameManager.Manager.firstBallHolder;
            secondBallHolder = GameManager.Manager.secondBallHolder;
            firstBoarder = GameManager.Manager.firstBoarder;
            secondBoarder = GameManager.Manager.secondBoarder;
            lives = GameManager.lives;

            playerOneStartPos = firstBallHolder.parent.gameObject.transform.position;
            playerTwoStartPos = secondBallHolder.parent.gameObject.transform.position;

            GameManager.Manager.livesTextFirst.text = "Lives left: " + lives;
            GameManager.Manager.livesTextSecond.text = "Lives left: " + lives;

            GetBlockSpawn(0f);
            HeartSpawn();

            foreach(GameObject block in blocks)
            {
                block.transform.rotation = Quaternion.Euler(GetRandomRotation());
            }
        }

        private void Update()
        {
            EndGame();
            WinCondition();
        }

        //Destroy blocks on collision with ball
        public IEnumerator SetDestroy(GameObject objectTriggered)
        {
            if (ball != null && objectTriggered.GetComponent<Block>() != null)
            {
                blocks.Remove(objectTriggered);
                Destroy(objectTriggered);
                
                yield return new WaitForSeconds(0f);
            }
        }

        //Life and region control for ball
        public IEnumerator SetCross(GameObject objectTriggered)
        {

            if (ball != null && objectTriggered.GetComponent<GetTrigger>().GetTriggeredObject == TriggeredControl.Boarder)
            {
                if (objectTriggered == firstBoarder)
                {
                    ball.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                    ball.transform.position = firstBallHolder.transform.position;
                    ball.transform.rotation = firstBallHolder.transform.rotation;

                    Destroy(heartListFirst[heartListFirst.Count - 1]);
                    Destroy(heartListSecond[heartListSecond.Count - 1]);

                    heartListFirst.RemoveAt(heartListFirst.Count - 1);
                    heartListSecond.RemoveAt(heartListSecond.Count - 1);

                    lives--;

                    GameManager.Manager.livesTextFirst.text = "Lives left: " + lives;
                    GameManager.Manager.livesTextSecond.text = "Lives left: " + lives;
                }
                else if (objectTriggered == secondBoarder)
                {
                    ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    ball.transform.position = secondBallHolder.transform.position;
                    ball.transform.rotation = secondBallHolder.transform.rotation;

                    Destroy(heartListFirst[heartListFirst.Count - 1]);
                    Destroy(heartListSecond[heartListSecond.Count - 1]);

                    heartListFirst.RemoveAt(heartListFirst.Count - 1);
                    heartListSecond.RemoveAt(heartListSecond.Count - 1);

                    lives--;

                    GameManager.Manager.livesTextFirst.text = "Lives left: " + lives;
                    GameManager.Manager.livesTextSecond.text = "Lives left: " + lives;
                }

                yield return new WaitForSeconds(0f);
            }
        }

        //Rotation for blocks on start
        private Vector3 GetRandomRotation()
        {
            float x = Random.Range(0f, 10f);
            float y = Random.Range(0f, 10f);
            float z = Random.Range(0f, 10f);

            return new Vector3(x, y, z);
        }

        private Vector3 GetRandomPosition(float leftBoarder, float rightBoarder, float topBoarder, float bottomBoarder, float frontBoarder, float backBoarder)
        {
            float x = Random.Range(leftBoarder, rightBoarder);
            float y = Random.Range(topBoarder, bottomBoarder);
            float z = Random.Range(frontBoarder, backBoarder);

            return new Vector3(x, y, z);
        }

        private void GetBlockSpawn(float stepForNextLevel)
        {
            for(int i=1; i<= GameManager.Manager.quantity; i++)
            {
                foreach(GameObject blockType in blocksTypes)
                {
                    GameObject blockToSpawn = Instantiate(blockType, GetRandomPosition
                        (levelCenter.x + stepFromCenter + stepForNextLevel, levelCenter.x - stepFromCenter + stepForNextLevel,
                        levelCenter.y + stepFromCenter + stepForNextLevel, levelCenter.y - stepFromCenter + stepForNextLevel,
                        levelCenter.z + stepFromCenterLength + stepForNextLevel, levelCenter.z - stepFromCenterLength + stepForNextLevel),
                        transform.rotation, GameManager.Manager.blocksParent);

                    blocks.Add(blockToSpawn);
                }
            }
        }

        //Condition on loose game
        private void EndGame()
        {
            if(lives <= 0)
            {
                ball.SetActive(false);
                Debug.Log("Game ended. You lost!");
                Debug.Log("Restart the game");


            }
        }

        //Condition on win game
        private void WinCondition()
        {
            if(blocks.Count == 0)
            {
                Debug.Log("You Won!");
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                NextLevel();
            }
        }

        //Activating next level on win
        private void NextLevel()
        {
            if (nextLevel.activeSelf == false)
            {
                nextLevel.SetActive(true);
                GameManager.Manager.GameDifficulty(GameManager.Manager.difficulty);

                GetBlockSpawn(nextLevelStep);
                HeartSpawn();

                foreach (GameObject block in blocks)
                {
                    block.transform.rotation = Quaternion.Euler(GetRandomRotation());
                }

                Debug.Log("Next level is ready. Player 1's turn!");
                MovePlayers();
                SetBallOnStart();
            }
            else
            {
                Debug.Log("You completed all levels. Congrats!");
            }
        }


        //Moving players platform on win
        private void MovePlayers()
        {
            var playerOne = firstBallHolder.parent.gameObject;
            var playerTwo = secondBallHolder.parent.gameObject;

            playerOne.transform.position = new Vector3(playerOneStartPos.x + nextLevelStep, playerOneStartPos.y, playerOneStartPos.z);
            playerTwo.transform.position = new Vector3(playerTwoStartPos.x + nextLevelStep, playerTwoStartPos.y, playerTwoStartPos.z);
        }


        //Set ball to start position on win
        private void SetBallOnStart()
        {
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            ball.transform.position = firstBallHolder.transform.position;

        }

        //spawning the amount of heart image into the game
        private void HeartSpawn()
        {
            for(int i =0; i<lives; i++)
            {
                GameObject heartFirst = Instantiate(heartPrefab, new Vector3(GameManager.Manager.heartHolderFirst.position.x + i * heartStepCanvas, GameManager.Manager.heartHolderFirst.position.y, GameManager.Manager.heartHolderFirst.position.z)
                    , transform.rotation, GameManager.Manager.heartHolderFirst);

                heartListFirst.Add(heartFirst);

                GameObject heartSecond = Instantiate(heartPrefab, new Vector3(GameManager.Manager.heartHolderFirst.position.x + i * heartStepCanvas, GameManager.Manager.heartHolderFirst.position.y, GameManager.Manager.heartHolderFirst.position.z)
                     , transform.rotation, GameManager.Manager.heartHolderSecond);

                heartListSecond.Add(heartSecond);

            }
        }
    }
}