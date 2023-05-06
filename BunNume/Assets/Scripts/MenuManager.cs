using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{ 
    private static MenuManager _instance;

    public static MenuManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MenuManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Start game
            SceneManager.LoadScene(0);
        }
    }

    public void GetBackToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
