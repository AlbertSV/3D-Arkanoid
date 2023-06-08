using System.Collections;
using System.Collections.Generic;
using Arkanoid;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class BallControl : MonoBehaviour
{
    #region
    private MoveContrl _playerMoves;
    private GameControl _gameControl;
    private GameObject _ball;
    private Transform _firstBallHolder;
    private Transform _secondBallHolder;
    private Vector3 _lastVelocity;

    private bool _needParent = true;

    private float _velocityMultiplyer = 1.0f;
    #endregion


    public float VelocityMultiplyer
    {
        get { return _velocityMultiplyer; }
        set { _velocityMultiplyer = value; }
    }


    private void Awake()
    {
        _gameControl = gameObject.AddComponent<GameControl>();
        _playerMoves = new MoveContrl();
    }

    private void Start()
    {
        _firstBallHolder = GameManager.Manager.firstBallHolder;
        _secondBallHolder = GameManager.Manager.secondBallHolder;
        _ball = GameManager.Manager.ball;
    }

    private void Update()
    {
        _lastVelocity = _ball.GetComponent<Rigidbody>().velocity;
        SetBallParent();
    }


    private void OnEnable()
    {
        _playerMoves.PlayerController.Enable();

        _playerMoves.PlayerController.PlayerSecondShoot.performed += OnShoot;
        _playerMoves.PlayerController.PlayerFirstShoot.performed += OnShoot;

    }
    

    private void OnCollisionEnter(Collision collision)
    {
        var trigger = collision.gameObject.GetComponent<GetTrigger>();

        if (trigger != null)
        {
            if (trigger.GetTriggeredObject == TriggeredControl.Block)
            {
                _ball.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                SetBallBounce(collision);
                StartCoroutine(_gameControl.SetDestroy(collision.gameObject));
            }

            else if (trigger.GetTriggeredObject == TriggeredControl.Boarder)
            {
                StartCoroutine(_gameControl.SetCross(collision.gameObject));
            }
            else
            {
                _ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

                SetBallBounce(collision);
            }
        }
    }

    private void OnDisable()
    {
        _playerMoves.PlayerController.Disable();
        _playerMoves.PlayerController.PlayerFirstShoot.performed -= OnShoot;
        _playerMoves.PlayerController.PlayerSecondShoot.performed -= OnShoot;
    }

    //Activate ball shooting
    private void OnShoot(CallbackContext context)
    {
        BallShoot();
    }

    //Shoot ball on key down
    private void BallShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightShift))
        {
            _ball.GetComponent<Rigidbody>().isKinematic = false;
            _ball.transform.SetParent(null);

            SetBallMove();
        }

    }

    //Control the ball velocity and movement
    private void SetBallMove()
    {
        _ball.GetComponent<Rigidbody>().velocity = (GameManager.ballSpeed * transform.forward * Time.deltaTime);
    }


    //Set ball to start player position
    private void SetBallParent()
    {
        if (_ball.transform.position == _firstBallHolder.position && _needParent)
        {
            _ball.GetComponent<Rigidbody>().isKinematic = true;
            _ball.transform.parent = _firstBallHolder;
            if (_ball.transform.parent != null)
            {
                _needParent = false;
            }
        }
        else if (_ball.transform.position == _secondBallHolder.position && _needParent)
        {
            _ball.GetComponent<Rigidbody>().isKinematic = true;
            _ball.transform.SetParent(_secondBallHolder);
            if (_ball.transform.parent != null)
            {
                _needParent = false;
            }
        }
        else if (_ball.transform.position != _secondBallHolder.position || _ball.transform.position != _firstBallHolder.position)
        {
            _needParent = true;
        }


    }

    //Add bouncing for the ball
    private void SetBallBounce(Collision coll)
    {
        if (VelocityMultiplyer <= 1.5)
        {
            VelocityMultiplyer += 0.1f;
        }
        var direction = Vector3.Reflect(_lastVelocity.normalized, coll.contacts[0].normal);

        _ball.GetComponent<Rigidbody>().velocity = (direction * GameManager.ballSpeed * Time.deltaTime * VelocityMultiplyer);

    }
}
