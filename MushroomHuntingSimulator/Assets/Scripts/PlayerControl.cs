using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlMode { VR_SET, KEYBOARD_AND_MOUSE }

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private ControlMode controlMode;

    public void DeactivateControl()
    {
        if (controlMode == ControlMode.KEYBOARD_AND_MOUSE)
        {
            DisableKeyboardAndMouseMovement();
            DisableMushroomCollectionKeyboardAndMouse();
        }
        else if (controlMode == ControlMode.VR_SET)
        {
            DisableVRSetWalking();
            DisableMushroomCollectionVRSet();
        }
    }

    public void ActivateRestartInput()
    {
        if (controlMode == ControlMode.KEYBOARD_AND_MOUSE)
            EnableRestartGameKeyboardAndMouse();
        else if (controlMode == ControlMode.VR_SET)
            EnableRestartGameVRSet();
    }

    private void Start()
    {
        InitMovement();
    }

    private void InitMovement()
    {
        if (controlMode == ControlMode.VR_SET)
        {
            DisableKeyboardAndMouseMovement();
            DisableMushroomCollectionKeyboardAndMouse();
        }
        else if (controlMode == ControlMode.KEYBOARD_AND_MOUSE)
        {
            DisableVRSetMovement();
            DisableHands();
        }
    }
    
    private void DisableKeyboardAndMouseMovement()
    {
        GetComponent<PlayerMovementKeyboardAndMouseController>().enabled = false;
    }
    
    private void DisableMushroomCollectionKeyboardAndMouse()
    {
        GetComponent<MushroomCollectionKeyboardAndMouseController>().enabled = false;
    }
    
    private void EnableRestartGameKeyboardAndMouse()
    {
        GetComponent<RestartGameKeyboardAndMouseController>().enabled = true;
    }

    private void DisableVRSetWalking()
    {
        GetComponent<PlayerMovementVRSetController>().DisableWalkingMovement();
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

    private void DisableMushroomCollectionVRSet()
    {
        GameObject.Find("LeftHand").GetComponent<MushroomCollectionVRSetController>().enabled = false;
        GameObject.Find("RightHand").GetComponent<MushroomCollectionVRSetController>().enabled = false;
    }

    private void EnableRestartGameVRSet()
    {
        GetComponent<RestartGameVRSetController>().enabled = true;
    }
}
