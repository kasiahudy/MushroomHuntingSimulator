using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAndRestartGameKeyboardAndMouseController : MonoBehaviour
{
    [SerializeField]
    private KeyCode startOrRestartKey = KeyCode.Space;

    private GameManager gameManager;

    void Start()
    {
        LoadGameManager();
    }

    void Update()
    {
        if (Input.GetKeyUp(startOrRestartKey))
            gameManager.RestartGame();
    }

    private void LoadGameManager()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
