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
            GetComponent<PlayerMovementMouseAndKeyboardController>().enabled = false;
        else if (controlMode == ControlMode.MOUSE_AND_KEYBOARD)
            GetComponent<PlayerMovementVRSetController>().enabled = false;
    }
}
