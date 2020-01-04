using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGameKeyboardAndMouseController : MonoBehaviour
{
    [SerializeField]
    private KeyCode restartKey = KeyCode.Space;

    private GameManager gameManager;

    void Start()
    {
        LoadGameManager();
    }

    void Update()
    {
        if (Input.GetKeyUp(restartKey))
            gameManager.RestartGame();
    }

    private void LoadGameManager()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
