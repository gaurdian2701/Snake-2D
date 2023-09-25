using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform snakeBody;
    [SerializeField] private List<Transform> bodyList;

    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Vector3 playerPos;
    private ParticleController particleController;


    public static Action sizeGrown;
    public static Action sizeDecreased;
    public static Action InitiateGameOverCondition;
    public static Action GamePaused;

    public bool isInvincible;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        particleController = GetComponent<ParticleController>(); 
        
        moveDirection.x = 0f;
        moveDirection.y = 1f;
        isInvincible = false;
    }

    private void Start()
    {
        bodyList.Add(this.transform);
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
        this.transform.up = moveDirection;

        MoveBodies();
    }

    private void MoveBodies()
    {
        for (int i = bodyList.Count - 1; i > 0; i--)
        {
            bodyList[i].position = bodyList[i - 1].position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckForGameOver(collision))
            GameOver();
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ToggleInvincibility() => isInvincible = !isInvincible;

    public void MoveSideWays(InputAction.CallbackContext context)
    {
        if (context.performed && moveDirection.y != 0f)
        {
            moveDirection.x = context.ReadValue<float>();
            moveDirection.y = 0f;
        }
    }

    public void MoveUpAndDown(InputAction.CallbackContext context)
    {
        if (context.performed && moveDirection.x != 0f)
        {
            moveDirection.y = context.ReadValue<float>();
            moveDirection.x = 0f;
        }
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GamePaused.Invoke();

            if (LevelLoader.Instance)
                LevelLoader.Instance.PauseScene();
        }
    }

    public void IncreaseSize()
    {
        sizeGrown.Invoke();

        particleController.EnableParticles(Color.red);

        Transform body = Instantiate(this.snakeBody);
        body.position = bodyList[bodyList.Count - 1].position;

        bodyList.Add(body);
    }

    public void DecreaseSize()
    {
        if (OnlySnakeHeadPresent())
        {
            GameOver();
            return;
        }

        else if (isInvincible)
            return;

        particleController.EnableParticles(Color.green);  
        sizeDecreased.Invoke();

        GameObject body = bodyList.ElementAt(bodyList.Count - 1).gameObject;
        bodyList.RemoveAt(bodyList.Count - 1);

        Destroy(body);
    }

    public void WrapPosition()
    {
        playerPos = transform.position;
        float xVal = playerPos.x;
        float yVal = playerPos.y;

        if (transform.up.x == 0f)
            PerformYWrapping(xVal, yVal);

        else
            PerformXWrapping(xVal, yVal);
    }

    private bool CheckForGameOver(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Snake") && IsTooClose(collision) && !isInvincible || collision.gameObject.GetComponent<PlayerController>())
            return true;

        return false;
    }
    private bool IsTooClose(Collider2D collision)
    {
        if (Vector3.Distance(transform.position, collision.transform.position) < 0.1f)
            return true;

        return false;
    }

    private bool OnlySnakeHeadPresent()
    {
        if (bodyList.Count == 1)
            return true;

        return false;
    }

    private void GameOver()
    {
        InitiateGameOverCondition.Invoke();
        GetComponent<PlayerInput>().enabled = false;
        moveSpeed = 0f;
    }

    private void PerformXWrapping(float xVal, float yVal)
    {
        playerPos = new Vector3(-xVal, yVal, 0f);
        transform.position = playerPos;
    }

    private void PerformYWrapping(float xVal, float yVal)
    {
        playerPos = new Vector3(xVal, -yVal, 0f);
        transform.position = playerPos;
    }
}
