using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;

    [SerializeField] private float playerSize = 10;
    [SerializeField] private float playerHeight = 2.5f;
    [SerializeField] private GameInput _gameInput;

    private bool _isWalking;

    //private Vector3 _lastInteractDirection;
    //private Vector3 _lastInteractDirection;
    public Vector3 inputDirection;
    public bool bomb;
    public bool rewind;
    public GameObject BombPrefab;


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void Start()
    {
        _gameInput.OnRewindAction += GameInputOnRewindAction;
    }


    //Bomb
    private void GameInputOnRewindAction(object sender, EventArgs e)
    {
        float interactDistance = 2f;
        var inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
    }

    public bool IsWalking()
    {
        return _isWalking;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }

    public void OnBomb(InputAction.CallbackContext context)
    {
        bomb = context.action.triggered;
    }

    public void OnRewind(InputAction.CallbackContext context)
    {
        rewind = context.action.triggered;
    }

    private void HandleMovement()
    {
        Vector3 position = transform.position;
        float moveDistance = _moveSpeed * Time.deltaTime;
        // inputDirection = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputDirection.x, inputDirection.y, inputDirection.y);
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

        if (bomb)
        {
            BombTriggered();
        }

        if (rewind)
        {
            Debug.Log("Rewinded "+transform.name);
        }

        _isWalking = moveDir != Vector3.zero;
        //transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotateSpeed);
    }

    public void BombTriggered()
    {
        Instantiate(BombPrefab, transform);
    }
}