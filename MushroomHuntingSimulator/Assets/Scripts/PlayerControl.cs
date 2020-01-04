using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlMode { VR_SET, KEYBOARD_AND_MOUSE }

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private ControlMode controlMode;

    public void ActivateControl()
    {
        if (controlMode == ControlMode.KEYBOARD_AND_MOUSE)
        {
            EnableKeyboardAndMouseMovement();
            EnableMushroomCollectionKeyboardAndMouse();
        }
        else if (controlMode == ControlMode.VR_SET)
        {
            EnableMushroomCollectionVRSet();
        }
    }

    public void DeactivateControl()
    {
        if (controlMode == ControlMode.KEYBOARD_AND_MOUSE)
        {
            DisableKeyboardAndMouseMovement();
            DisableMushroomCollectionKeyboardAndMouse();
        }
        else if (controlMode == ControlMode.VR_SET)
        {
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

    public void DeactivateRestartInput()
    {
        if (controlMode == ControlMode.KEYBOARD_AND_MOUSE)
            DisableRestartGameKeyboardAndMouse();
        else if (controlMode == ControlMode.VR_SET)
            DisableRestartGameVRSet();
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

    private void EnableKeyboardAndMouseMovement()
    {
        GetComponent<PlayerMovementKeyboardAndMouseController>().enabled = true;
    }

    private void EnableMushroomCollectionKeyboardAndMouse()
    {
        GetComponent<MushroomCollectionKeyboardAndMouseController>().enabled = true;
    }

    private void EnableRestartGameKeyboardAndMouse()
    {
        GetComponent<RestartGameKeyboardAndMouseController>().enabled = true;
    }

    private void EnableRestartGameVRSet()
    {
        GetComponent<RestartGameVRSetController>().enabled = true;
    }

    private void EnableMushroomCollectionVRSet()
    {
        GameObject.Find("LeftHand").GetComponent<MushroomCollectionVRSetController>().enabled = true;
        GameObject.Find("RightHand").GetComponent<MushroomCollectionVRSetController>().enabled = true;
    }

    private void DisableKeyboardAndMouseMovement()
    {
        GetComponent<PlayerMovementKeyboardAndMouseController>().enabled = false;
    }

    private void DisableMushroomCollectionKeyboardAndMouse()
    {
        GetComponent<MushroomCollectionKeyboardAndMouseController>().enabled = false;
    }

    private void DisableRestartGameKeyboardAndMouse()
    {
        GetComponent<RestartGameKeyboardAndMouseController>().enabled = false;
    }

    private void DisableRestartGameVRSet()
    {
        GetComponent<RestartGameVRSetController>().enabled = false;
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
}
