using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlMode { VR_SET, MOUSE_AND_KEYBOARD }

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private ControlMode controlMode;

    private void Start()
    {
        if (controlMode == ControlMode.VR_SET)
            DisableMouseAndKeyboardMovement();
        else if (controlMode == ControlMode.MOUSE_AND_KEYBOARD)
        {
            DisableVRSetMovement();
            DisableHands();
        }
    }
    
    private void DisableMouseAndKeyboardMovement()
    {
        GetComponent<PlayerMovementMouseAndKeyboardController>().enabled = false;
    }

    private void DisableVRSetMovement()
    {
        GetComponent<PlayerMovementVRSetController>().enabled = false;
    }

    private void DisableHands()
    {
        GameObject.Find("LeftHand").SetActive(false);
        GameObject.Find("RightHand").SetActive(false);
    }
}
