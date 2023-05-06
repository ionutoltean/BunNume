using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;

    [SerializeField] private float playerSize = 10;
    [SerializeField] private float playerHeight = 2.5f;
    [SerializeField] private GameInput _gameInput;
    private bool _isWalking;
    private Vector3 _lastInteractDirection;


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void Start()
    {
        _gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        float interactDistance = 2f;
        var inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            _lastInteractDirection = moveDir;
        }
    }

    public bool IsWalking()
    {
        return _isWalking;
    }

    private void HandleMovement()
    {
        Vector3 position = transform.position;
        float moveDistance = _moveSpeed * Time.deltaTime;
        var inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y, inputVector.y);
        bool canMove = !Physics.CapsuleCast(position, position + Vector3.up * playerHeight,
            playerSize, moveDir, moveDistance);


        if (!canMove)
        {
            /// X
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(position, position + Vector3.up * playerHeight,
                playerSize, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                //z
                Vector3 moveDirZ = new Vector3(0, moveDir.z, moveDir.z).normalized;

                canMove = !Physics.CapsuleCast(position, position + Vector3.up * playerHeight,
                    playerSize, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDistance * moveDir;
        }

        _isWalking = moveDir != Vector3.zero;
        //transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotateSpeed);
    }
}