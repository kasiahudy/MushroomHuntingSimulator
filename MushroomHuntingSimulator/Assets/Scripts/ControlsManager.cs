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

    public void ActivateControl()
    {
        if (controlMode == ControlMode.KEYBOARD_AND_MOUSE)
        {
            player.GetComponent<PlayerMovementKeyboardAndMouseController>().enabled = true;
            player.GetComponent<MushroomCollectionKeyboardAndMouseController>().enabled = true;
        }
        else if (controlMode == ControlMode.VR_SET)
        {
            player.GetComponent<PlayerMovementVRSetController>().EnableWalkingMovement();
            GameObject.Find("LeftHand").GetComponent<MushroomCollectionVRSetController>().enabled = true;
            GameObject.Find("RightHand").GetComponent<MushroomCollectionVRSetController>().enabled = true;
        }
    }

    public void DeactivateControl()
    {
        if (controlMode == ControlMode.KEYBOARD_AND_MOUSE)
        {
            player.GetComponent<PlayerMovementKeyboardAndMouseController>().enabled = false;
            player.GetComponent<MushroomCollectionKeyboardAndMouseController>().enabled = false;
        }
        else if (controlMode == ControlMode.VR_SET)
        {
            player.GetComponent<PlayerMovementVRSetController>().DisableWalkingMovement();
            GameObject.Find("LeftHand").GetComponent<MushroomCollectionVRSetController>().enabled = false;
            GameObject.Find("RightHand").GetComponent<MushroomCollectionVRSetController>().enabled = false;
        }
    }

    private void Start()
    {
        if (controlMode == ControlMode.KEYBOARD_AND_MOUSE)
        {
            vrCameraRig.SetActive(false);
            player.GetComponent<StartAndRestartGameKeyboardAndMouseController>().enabled = true;
        }
        else if (controlMode == ControlMode.VR_SET)
        {
            keyboardAndMouseCameraObject.SetActive(false);
            keyboardAndMouseUI.SetActive(false);
            player.GetComponent<PlayerMovementVRSetController>().enabled = true;
            player.GetComponent<StartAndRestartGameVRSetController>().enabled = true;
            DeactivateControl();
        }
    }
}
