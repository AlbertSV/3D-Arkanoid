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

        private List<GameObject> _blocks;
        private List<GameObject> _blocksTypes;
        private List<GameObject> _heartListFirst;
        private List<GameObject> _heartListSecond;

        private Vector3 _playerOneStartPos;
        private Vector3 _playerTwoStartPos;
        private Vector3 _levelCenter = new Vector3(0f, 7.5f, 0f);

        private float _nextLevelStep = -20f;
        private float _stepFromCenter = 1.5f;
        private float _stepFromCenterLength = 3.5f;
        private float _heartStepCanvas = -15f;

        private Transform _firstBallHolder;
        private Transform _secondBallHolder;

        private GameObject _nextLevel;
        private GameObject _ball;
        private GameObject _firstBoarder;
        private GameObject _secondBoarder;
        private GameObject _heartPrefab;

        private int _lives;
        #endregion

        private void Awake()
        {
            _heartPrefab = Resources.Load<GameObject>("Prefabs/Heart");
            _heartListFirst = new List<GameObject>();
            _heartListSecond = new List<GameObject>();

            _blocksTypes = new List<GameObject>
            {
                Resources.Load<GameObject>("Prefabs/Block Type1"),
                Resources.Load<GameObject>("Prefabs/Block Type2"),
                Resources.Load<GameObject>("Prefabs/Block Type3"),
                Resources.Load<GameObject>("Prefabs/Block Type4"),
            };
            _blocks = new List<GameObject>();

            _nextLevel = FindObjectOfType<LevelTwo>().gameObject;
            _nextLevel.SetActive(false);

        }
        private void Start()
        {
            _ball = GameManager.Manager.ball;
            _firstBallHolder = GameManager.Manager.firstBallHolder;
            _secondBallHolder = GameManager.Manager.secondBallHolder;
            _firstBoarder = GameManager.Manager.firstBoarder;
            _secondBoarder = GameManager.Manager.secondBoarder;
            _lives = GameManager.lives;

            _playerOneStartPos = _firstBallHolder.parent.gameObject.transform.position;
            _playerTwoStartPos = _secondBallHolder.parent.gameObject.transform.position;

            GameManager.Manager.livesTextFirst.text = "Lives left: " + _lives;
            GameManager.Manager.livesTextSecond.text = "Lives left: " + _lives;

            GetBlockSpawn(0f);
            HeartSpawn();

            foreach(GameObject block in _blocks)
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
            if (_ball != null && objectTriggered.GetComponent<Block>() != null)
            {
                _blocks.Remove(objectTriggered);
                Destroy(objectTriggered);
                
                yield return new WaitForSeconds(0f);
            }
        }

        //Life and region control for ball
        public IEnumerator SetCross(GameObject objectTriggered)
        {

            if (_ball != null && objectTriggered.GetComponent<GetTrigger>().GetTriggeredObject == TriggeredControl.Boarder)
            {
                if (objectTriggered == _firstBoarder)
                {
                    _ball.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                    _ball.transform.position = _firstBallHolder.transform.position;
                    _ball.transform.rotation = _firstBallHolder.transform.rotation;

                    Destroy(_heartListFirst[_heartListFirst.Count - 1]);
                    Destroy(_heartListSecond[_heartListSecond.Count - 1]);

                    _heartListFirst.RemoveAt(_heartListFirst.Count - 1);
                    _heartListSecond.RemoveAt(_heartListSecond.Count - 1);

                    _lives--;

                    GameManager.Manager.livesTextFirst.text = "Lives left: " + _lives;
                    GameManager.Manager.livesTextSecond.text = "Lives left: " + _lives;
                }
                else if (objectTriggered == _secondBoarder)
                {
                    _ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    _ball.transform.position = _secondBallHolder.transform.position;
                    _ball.transform.rotation = _secondBallHolder.transform.rotation;

                    Destroy(_heartListFirst[_heartListFirst.Count - 1]);
                    Destroy(_heartListSecond[_heartListSecond.Count - 1]);

                    _heartListFirst.RemoveAt(_heartListFirst.Count - 1);
                    _heartListSecond.RemoveAt(_heartListSecond.Count - 1);

                    _lives--;

                    GameManager.Manager.livesTextFirst.text = "Lives left: " + _lives;
                    GameManager.Manager.livesTextSecond.text = "Lives left: " + _lives;
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
                foreach(GameObject blockType in _blocksTypes)
                {
                    GameObject blockToSpawn = Instantiate(blockType, GetRandomPosition
                        (_levelCenter.x + _stepFromCenter + stepForNextLevel, _levelCenter.x - _stepFromCenter + stepForNextLevel,
                        _levelCenter.y + _stepFromCenter + stepForNextLevel, _levelCenter.y - _stepFromCenter + stepForNextLevel,
                        _levelCenter.z + _stepFromCenterLength + stepForNextLevel, _levelCenter.z - _stepFromCenterLength + stepForNextLevel),
                        transform.rotation, GameManager.Manager.blocksParent);

                    _blocks.Add(blockToSpawn);
                }
            }
        }

        //Condition on loose game
        private void EndGame()
        {
            if(_lives <= 0)
            {
                _ball.SetActive(false);
                Debug.Log("Game ended. You lost!");
                Debug.Log("Restart the game");


            }
        }

        //Condition on win game
        private void WinCondition()
        {
            if(_blocks.Count == 0)
            {
                Debug.Log("You Won!");
                _ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                NextLevel();
            }
        }

        //Activating next level on win
        private void NextLevel()
        {
            if (_nextLevel.activeSelf == false)
            {
                _nextLevel.SetActive(true);
                GameManager.Manager.GameDifficulty(GameManager.Manager.difficulty);

                GetBlockSpawn(_nextLevelStep);
                HeartSpawn();

                foreach (GameObject block in _blocks)
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
            var playerOne = _firstBallHolder.parent.gameObject;
            var playerTwo = _secondBallHolder.parent.gameObject;

            playerOne.transform.position = new Vector3(_playerOneStartPos.x + _nextLevelStep, _playerOneStartPos.y, _playerOneStartPos.z);
            playerTwo.transform.position = new Vector3(_playerTwoStartPos.x + _nextLevelStep, _playerTwoStartPos.y, _playerTwoStartPos.z);
        }


        //Set ball to start position on win
        private void SetBallOnStart()
        {
            _ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            _ball.transform.position = _firstBallHolder.transform.position;

        }

        //spawning the amount of heart image into the game
        private void HeartSpawn()
        {
            for(int i =0; i<_lives; i++)
            {
                GameObject heartFirst = Instantiate(_heartPrefab, new Vector3(GameManager.Manager.heartHolderFirst.position.x + i * _heartStepCanvas, GameManager.Manager.heartHolderFirst.position.y, GameManager.Manager.heartHolderFirst.position.z)
                    , transform.rotation, GameManager.Manager.heartHolderFirst);

                _heartListFirst.Add(heartFirst);

                GameObject heartSecond = Instantiate(_heartPrefab, new Vector3(GameManager.Manager.heartHolderFirst.position.x + i * _heartStepCanvas, GameManager.Manager.heartHolderFirst.position.y, GameManager.Manager.heartHolderFirst.position.z)
                     , transform.rotation, GameManager.Manager.heartHolderSecond);

                _heartListSecond.Add(heartSecond);

            }
        }
    }
}