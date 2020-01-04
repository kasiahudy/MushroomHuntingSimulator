using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementKeyboardAndMouseController : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivityX = 3.0f;
    [SerializeField]
    private float mouseSensitivityY = 3.0f;
    [SerializeField]
    private float mouseLookMinimumX = -360.0f;
    [SerializeField]
    private float mouseLookMaximumX = 360.0f;
    [SerializeField]
    private float mouseLookMinimumY = -60.0f;
    [SerializeField]
    private float mouseLookMaximumY = 60.0f;
    [SerializeField]
    private KeyCode moveForwardKey = KeyCode.W;
    [SerializeField]
    private KeyCode moveBackwardKey = KeyCode.S;
    [SerializeField]
    private KeyCode moveLeftKey = KeyCode.A;
    [SerializeField]
    private KeyCode moveRightKey = KeyCode.D;
    [SerializeField]
    private float movementSpeed = 3.0f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private Quaternion originalRotation;

    private void Start()
    {
        SaveOriginalRotation();
    }

    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    private void SaveOriginalRotation()
    {
        originalRotation = transform.rotation;
    }

    private void HandleMouseLook()
    {
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivityX;
        rotationY += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        rotationX = ClampAngle(rotationX, mouseLookMinimumX, mouseLookMaximumX);
        rotationY = ClampAngle(rotationY, mouseLookMinimumY, mouseLookMaximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
        transform.localRotation = originalRotation * xQuaternion * yQuaternion;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;

        return Mathf.Clamp(angle, min, max);
    }

    private void HandleMovement()
    {
        if (KeyPressed(moveForwardKey))
            MoveForward();
        if (KeyPressed(moveBackwardKey))
            MoveBackward();
        if (KeyPressed(moveLeftKey))
            MoveLeft();
        if (KeyPressed(moveRightKey))
            MoveRight();
    }

    private bool KeyPressed(KeyCode keycode)
    {
        return Input.GetKey(keycode);
    }

    private void MoveForward()
    {
        Vector3 moveDirection = transform.forward;
        moveDirection.y = 0;
        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }

    private void MoveBackward()
    {
        Vector3 moveDirection = -transform.forward;
        moveDirection.y = 0;
        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }

    private void MoveLeft()
    {
        Vector3 moveDirection = transform.forward;
        moveDirection.y = 0;
        moveDirection = Vector3.Cross(moveDirection, Vector3.up).normalized;
        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }

    private void MoveRight()
    {
        Vector3 moveDirection = -transform.forward;
        moveDirection.y = 0;
        moveDirection = Vector3.Cross(moveDirection, Vector3.up).normalized;
        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }
}
