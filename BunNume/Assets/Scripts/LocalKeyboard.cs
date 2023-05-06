using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalKeyboard : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    private PlayerInput player1;
    private PlayerInput player2;

    void Start()
    {
        player1 = PlayerInput.Instantiate(player1Prefab, controlScheme: "KeyboardArrows",
            pairWithDevice: Keyboard.current);
        player2 = PlayerInput.Instantiate(player2Prefab, controlScheme: "KeyboardWASD",
            pairWithDevice: Keyboard.current);
        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);
        //var player3 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: Gamepad.current);
        // var player3 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: Gamepad.current);
    }

    // Update is called once per frame
    void Update()
    {
        // if ((Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.A)) ||
        //     (Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.D)))
        //     player2.gameObject.SetActive(true);
        // if ((Input.GetKeyDown(KeyCode.DownArrow)) || (Input.GetKeyDown(KeyCode.UpArrow)) ||
        //     (Input.GetKeyDown(KeyCode.LeftArrow)) ||
        //     (Input.GetKeyDown(KeyCode.RightArrow)))
        //     player1.gameObject.SetActive(true);
    }
}