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
    private bool canMove;
    private bool onTerrain;


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
        //  var inputVector = _gameInput.GetMovementVectorNormalized();
        //  Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
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

    void OnTriggerEnter2D(Collider2D col)
    {
        // if (col.name.Contains("Terrain"))
        //     onTerrain = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        transform.position = Vector3.Lerp(transform.position, other.gameObject.transform.position, Time.deltaTime);
    }

    private void HandleMovement()
    {
        Vector3 position = transform.position;
        float moveDistance = _moveSpeed * Time.deltaTime;
        Vector3 moveDir = new Vector3(inputDirection.x, inputDirection.y, 0);


        canMove = true;
      //  if (onTerrain == false) return;

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
            Debug.Log("Rewinded " + transform.name);
        }

        _isWalking = moveDir != Vector3.zero;
        //transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotateSpeed);
    }

    public void BombTriggered()
    {
        Instantiate(BombPrefab, transform);
    }
}