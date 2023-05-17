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
        private GameObject nextLevel;

        #endregion

        private void Awake()
        {
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
            playerOneStartPos = GameManager.Manager.firstBallHolder.parent.gameObject.transform.position;
            playerTwoStartPos = GameManager.Manager.secondBallHolder.parent.gameObject.transform.position;

            GetBlockSpawn(0f);

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
            if (GameManager.Manager.ball != null && objectTriggered.GetComponent<Block>() != null)
            {
                blocks.Remove(objectTriggered);
                Destroy(objectTriggered);
                
                yield return new WaitForSeconds(0f);
            }
        }

        //Life and region control for ball
        public IEnumerator SetCross(GameObject objectTriggered)
        {

            if (GameManager.Manager.ball != null && objectTriggered.GetComponent<GetTrigger>().GetTriggeredObject == TriggeredControl.Boarder)
            {
                if (objectTriggered == GameManager.Manager.firstBoarder)
                {
                    GameManager.Manager.ball.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                    GameManager.Manager.ball.transform.position = GameManager.Manager.firstBallHolder.transform.position;
                    GameManager.Manager.ball.transform.rotation = GameManager.Manager.firstBallHolder.transform.rotation;
                    GameManager.Manager.lifes--;
                    Debug.Log("Lifes left:");
                    Debug.Log(GameManager.Manager.lifes);
                }
                else if (objectTriggered == GameManager.Manager.secondBoarder)
                {
                    GameManager.Manager.ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    GameManager.Manager.ball.transform.position = GameManager.Manager.secondBallHolder.transform.position;
                    GameManager.Manager.ball.transform.rotation = GameManager.Manager.secondBallHolder.transform.rotation;
                    GameManager.Manager.lifes--;
                    Debug.Log("Lifes left:");
                    Debug.Log(GameManager.Manager.lifes);
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
            if(GameManager.Manager.lifes <= 0)
            {
                GameManager.Manager.ball.SetActive(false);
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
                GameManager.Manager.ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                NextLevel();
            }
        }

        //Activating next level on win
        private void NextLevel()
        {
            if (nextLevel.activeSelf == false)
            {
                nextLevel.SetActive(true);
                GameManager.Manager.lifes = 3;

                GetBlockSpawn(nextLevelStep);

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
            var playerOne = GameManager.Manager.firstBallHolder.parent.gameObject;
            var playerTwo = GameManager.Manager.secondBallHolder.parent.gameObject;

            playerOne.transform.position = new Vector3(playerOneStartPos.x + nextLevelStep, playerOneStartPos.y, playerOneStartPos.z);
            playerTwo.transform.position = new Vector3(playerTwoStartPos.x + nextLevelStep, playerTwoStartPos.y, playerTwoStartPos.z);
        }


        //Set ball to start position on win
        private void SetBallOnStart()
        {
            GameManager.Manager.ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GameManager.Manager.ball.transform.position = GameManager.Manager.firstBallHolder.transform.position;

        }
    }
}