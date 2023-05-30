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
    private Transform firstBallHolder;
    private Transform secondBallHolder;
    private Vector3 ballDirection;
    private Vector3 lastVelocity;

    private bool needParent = true;

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
        firstBallHolder = GameManager.Manager.firstBallHolder;
        secondBallHolder = GameManager.Manager.secondBallHolder;
        ball = GameManager.Manager.ball;
    }

    private void Update()
    {
        lastVelocity = ball.GetComponent<Rigidbody>().velocity;
        SetBallParent();
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

        if (trigger != null)
        {
            if (trigger.GetTriggeredObject == TriggeredControl.Block)
            {
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                SetBallBounce(collision);
                StartCoroutine(gameControl.SetDestroy(collision.gameObject));
            }

            else if (trigger.GetTriggeredObject == TriggeredControl.Boarder)
            {
                StartCoroutine(gameControl.SetCross(collision.gameObject));
            }
            else
            {
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

                SetBallBounce(collision);
            }
        }
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

            SetBallMove();
        }

    }

    //Control the ball velocity and movement
    private void SetBallMove()
    {
        ball.GetComponent<Rigidbody>().velocity = (GameManager.ballSpeed * transform.forward * Time.deltaTime);
    }


    //Set ball to start player position
    private void SetBallParent()
    {
        if (ball.transform.position == firstBallHolder.position && needParent)
        {
            ball.GetComponent<Rigidbody>().isKinematic = true;
            ball.transform.parent = firstBallHolder;
            if (ball.transform.parent != null)
            {
                needParent = false;
            }
        }
        else if (ball.transform.position == secondBallHolder.position && needParent)
        {
            ball.GetComponent<Rigidbody>().isKinematic = true;
            ball.transform.SetParent(secondBallHolder);
            if (ball.transform.parent != null)
            {
                needParent = false;
            }
        }
        else if (ball.transform.position != secondBallHolder.position || ball.transform.position != firstBallHolder.position)
        {
            needParent = true;
        }


    }

    //Add bouncing for the ball
    private void SetBallBounce(Collision coll)
    {
        if (VelocityMultiplyer <= 1.5)
        {
            VelocityMultiplyer += 0.1f;
        }
        var direction = Vector3.Reflect(lastVelocity.normalized, coll.contacts[0].normal);

        ball.GetComponent<Rigidbody>().velocity = (direction * GameManager.ballSpeed * Time.deltaTime * VelocityMultiplyer);

    }
}
