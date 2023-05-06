using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalKeyboard : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerPrefab;

    void Start()
    {
        var player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "KeyboardArrows", pairWithDevice: Keyboard.current);
        var player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "KeyboardWASD", pairWithDevice: Keyboard.current);
        //var player3 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: Gamepad.current);
       // var player3 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: Gamepad.current);
    }

    // Update is called once per frame
    void Update()
    {
    }
}