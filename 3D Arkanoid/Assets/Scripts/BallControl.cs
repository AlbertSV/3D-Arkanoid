using System.Collections;
using System.Collections.Generic;
using Arkanoid;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class BallControl : MonoBehaviour
{
    #region
    private MoveContrl playerMoves;
    private GameObject ball;
    private Vector3 ballDirection;

    private bool needParent = true;
    private bool startShootMove = false;
    private bool startBounceMove = false;

    private float velocityMultiplyer = 1.0f;

    private GameControl gameControl;
    #endregion


    public float VelocityMultiplyer
    {
        get { return velocityMultiplyer; }
        set { velocityMultiplyer = value; }
    }


    private void Awake()
    {
        gameControl = gameObject.AddComponent<GameControl>();
        playerMoves = new MoveContrl();
    }

    private void Start()
    {
        ball = GameManager.Manager.ball;
    }

    private void Update()
    {
        SetBallParent();

        if(startShootMove && !startBounceMove)
        {
            SetBallMove(transform.forward);
        }
        else if(!startShootMove && startBounceMove)
        {
            SetBallMove(ballDirection, VelocityMultiplyer);
        }

    }


    private void OnEnable()
    {
        playerMoves.PlayerController.Enable();

        playerMoves.PlayerController.PlayerSecondShoot.performed += OnShoot;
        playerMoves.PlayerController.PlayerFirstShoot.performed += OnShoot;

    }
    

    private void OnCollisionEnter(Collision collision)
    {
        var trigger = collision.gameObject.GetComponent<GetTrigger>();
        Debug.Log(trigger);
        Debug.Log(collision.gameObject);
        if (trigger != null)
        {
            if (trigger.GetTriggeredObject == TriggeredControl.Block)
            {
                startShootMove = false;
                SetBallBounce(collision);
                startBounceMove = true;

                StartCoroutine(gameControl.SetDestroy(collision.gameObject));
            }

            else if (trigger.GetTriggeredObject == TriggeredControl.Boarder)
            {
                startShootMove = false;
                startBounceMove = false;

                StartCoroutine(gameControl.SetCross(collision.gameObject));
            }
            else
            {
                startShootMove = false;
                SetBallBounce(collision);
                startBounceMove = true;
            }
        }
        Debug.Log(startShootMove);
        Debug.Log(startBounceMove);
    }

    private void OnDisable()
    {
        playerMoves.PlayerController.Disable();
        playerMoves.PlayerController.PlayerFirstShoot.performed -= OnShoot;
        playerMoves.PlayerController.PlayerSecondShoot.performed -= OnShoot;
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
            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.transform.SetParent(null);

            startBounceMove = false;
            startShootMove = true;
        }

    }

    //Control the ball velocity and movement
    private void SetBallMove(Vector3 direction, float velocityMultiplayer = 1f)
    {
        ball.transform.position += (GameManager.Manager.ballSpeed * direction * Time.deltaTime * velocityMultiplyer);
    }


    //Set ball to start player position
    private void SetBallParent()
    {
        if (ball.transform.position == GameManager.Manager.firstBallHolder.position && needParent)
        {
            ball.GetComponent<Rigidbody>().isKinematic = true;
            ball.transform.parent = GameManager.Manager.firstBallHolder;
            if (ball.transform.parent != null)
            {
                needParent = false;
            }
        }
        else if (ball.transform.position == GameManager.Manager.secondBallHolder.position && needParent)
        {
            ball.GetComponent<Rigidbody>().isKinematic = true;
            ball.transform.SetParent(GameManager.Manager.secondBallHolder);
            if (ball.transform.parent != null)
            {
                needParent = false;
            }
        }
        else if (ball.transform.position != GameManager.Manager.secondBallHolder.position || ball.transform.position != GameManager.Manager.firstBallHolder.position)
        {
            needParent = true;
        }


    }

    //Add bouncing for the ball
    private void SetBallBounce(Collision coll)
    {

        if (VelocityMultiplyer <= GameManager.Manager.maxBallSpeed)
        {
            VelocityMultiplyer += GameManager.Manager.ballSpeedRise;
        }

        ballDirection = Vector3.Reflect(ball.transform.position.normalized, coll.contacts[0].normal);

    }
}
