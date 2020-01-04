using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RestartGameVRSetController : MonoBehaviour
{
    [SerializeField]
    private SteamVR_Action_Boolean restartPress;

    private GameManager gameManager;

    void Start()
    {
        LoadGameManager();
    }

    void Update()
    {
        if (RestartButtonPressed())
            gameManager.RestartGame();
    }

    private void LoadGameManager()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private bool RestartButtonPressed()
    {
        return restartPress.state;
    }
}
