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
    private ParticleSystem particle;

  

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        particle = GetComponent<ParticleSystem>();
        moveDirection.x = 1f;
        moveDirection.y = 0f;
    }

    private void Start()
    {
        bodyList.Add(this.transform);
    }

    private void OnDisable()
    {
  
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
        if (collision.gameObject.name.StartsWith("Snake") && Vector3.Distance(transform.position, collision.transform.position) < 0.1f)
            Debug.Log("GAME OVER");
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

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

    public void IncreaseSize()
    {
        EnableParticles(Color.red);
        Transform body = Instantiate(this.snakeBody);
        body.position = bodyList[bodyList.Count - 1].position;
        bodyList.Add(body);
    }

    public void DecreaseSize()
    {
        EnableParticles(Color.green);
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

    public void EnableParticles(Color color)
    {
        GetComponent<ParticleSystemRenderer>().material.color = color;
        particle.Play();
    }
}
