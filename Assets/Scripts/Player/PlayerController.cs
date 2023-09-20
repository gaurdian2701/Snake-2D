using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject snakeHead;
    [SerializeField] private Transform snakeBody;
    [SerializeField] private List<Transform> bodyList;

    private Vector2 moveDirection;
    private Rigidbody2D rb;
 

  

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection.x = 1f;
        moveDirection.y = 0f;
    }

    void Start()
    {
        ApplePickup.ApplePicked += IncreaseSize;
        bodyList.Add(transform);
    }

    private void OnDisable()
    {
        ApplePickup.ApplePicked -= IncreaseSize;
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
        snakeHead.transform.up = moveDirection;

        MoveBodies();
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

    private void MoveBodies()
    {
        for(int i = bodyList.Count - 1; i > 0; i--)
        {
            bodyList[i].position = bodyList[i-1].position;
        }
    }

    private void IncreaseSize()
    {
        Transform body = Instantiate(this.snakeBody);
        body.position = bodyList[bodyList.Count - 1].position;
        bodyList.Add(body);
    }

}
