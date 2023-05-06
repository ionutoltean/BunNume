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

   


   
}