using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
     private PlayerInputActions playernPlayerInputActions;
    public event EventHandler OnRewindAction;
   
    private void Awake()
    {
        playernPlayerInputActions = new PlayerInputActions();
        playernPlayerInputActions.Enable();
    
       // playernPlayerInputActions.PlayerWasd.Bomb.performed += BombTriggered;
      //  playernPlayerInputActions.PlayerWasd.Rewind.performed += TurnBackTime;
    }

    private void TurnBackTime(InputAction.CallbackContext obj)
    {
        OnRewindAction?.Invoke(this, EventArgs.Empty);
    }

   


    public Vector2 GetMovementVectorNormalized()
    {
    //    Vector2 inputVector = playernPlayerInputActions.PlayerWasd.Move.ReadValue<Vector2>();

      //  inputVector = inputVector.normalized;

        return Vector2.down; //inputVector;
    }
}