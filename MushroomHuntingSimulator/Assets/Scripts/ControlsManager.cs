using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlMode { VR_SET, KEYBOARD_AND_MOUSE }

public class ControlsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject vrCameraRig;
    [SerializeField]
    private GameObject keyboardAndMouseCameraObject;
    [SerializeField]
    private GameObject keyboardAndMouseUI;
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
            DisableKeyboardAndMouseCamera();
            DisableKeyboardAndMouseUI();
            DisableKeyboardAndMouseMovement();
            DisableMushroomCollectionKeyboardAndMouse();
        }
        else if (controlMode == ControlMode.KEYBOARD_AND_MOUSE)
        {
            DisableVRSet();
        }
    }

    private void DisableKeyboardAndMouseCamera()
    {
        keyboardAndMouseCameraObject.SetActive(false);
    }

    private void DisableKeyboardAndMouseUI()
    {
        keyboardAndMouseUI.SetActive(false);
    }

    private void DisableKeyboardAndMouseMovement()
    {
        player.GetComponent<PlayerMovementKeyboardAndMouseController>().enabled = false;
    }

    private void DisableMushroomCollectionKeyboardAndMouse()
    {
        player.GetComponent<MushroomCollectionKeyboardAndMouseController>().enabled = false;
    }

    private void EnableRestartGameKeyboardAndMouse()
    {
        player.GetComponent<RestartGameKeyboardAndMouseController>().enabled = true;
    }

    private void DisableVRSet()
    {
        vrCameraRig.SetActive(false);
        player.GetComponent<PlayerMovementVRSetController>().enabled = false;
    }

    private void DisableVRSetWalking()
    {
        player.GetComponent<PlayerMovementVRSetController>().DisableWalkingMovement();
    }   

    private void DisableMushroomCollectionVRSet()
    {
        GameObject.Find("LeftHand").GetComponent<MushroomCollectionVRSetController>().enabled = false;
        GameObject.Find("RightHand").GetComponent<MushroomCollectionVRSetController>().enabled = false;
    }

    private void EnableRestartGameVRSet()
    {
        player.GetComponent<RestartGameVRSetController>().enabled = true;
    }
}
