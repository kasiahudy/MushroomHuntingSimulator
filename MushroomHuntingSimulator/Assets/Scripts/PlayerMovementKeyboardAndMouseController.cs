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
    [SerializeField]
    private Camera camera;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private Vector3 originalPlayerPosition;
    private Vector3 lastValidPlayerPosition;
    private Quaternion originalCameraRotation;

    private void Start()
    {
        SaveOriginalPlayerPosition();
        SaveOriginalCameraRotation();
    }

    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleInvalidPosition();
    }

    private void SaveOriginalPlayerPosition()
    {
        originalPlayerPosition = transform.position;
    }
    
    private void SaveOriginalCameraRotation()
    {
        originalCameraRotation = camera.transform.rotation;
    }

    private void HandleMouseLook()
    {
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivityX;
        rotationY += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        rotationX = ClampAngle(rotationX, mouseLookMinimumX, mouseLookMaximumX);
        rotationY = ClampAngle(rotationY, mouseLookMinimumY, mouseLookMaximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
        camera.transform.localRotation = originalCameraRotation * xQuaternion * yQuaternion;
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
        Vector3 direction = transform.forward;
        MovePlayerByVector(CalculateMovementVectorWithDirection(direction));
    }

    private void MoveBackward()
    {
        Vector3 direction = -transform.forward;
        MovePlayerByVector(CalculateMovementVectorWithDirection(direction));
    }

    private void MoveLeft()
    {
        Vector3 direction = transform.forward;
        direction.y = 0;
        direction = Vector3.Cross(direction, Vector3.up).normalized;
        MovePlayerByVector(CalculateMovementVectorWithDirection(direction));
    }

    private void MoveRight()
    {
        Vector3 direction = -transform.forward;
        direction.y = 0;
        direction = Vector3.Cross(direction, Vector3.up).normalized;
        MovePlayerByVector(CalculateMovementVectorWithDirection(direction));
    }

    private Vector3 CalculateMovementVectorWithDirection(Vector3 direction)
    {
        return Vector3.zero + GetCameraOrientation() * (movementSpeed * direction) * Time.deltaTime;
    }

    private Quaternion GetCameraOrientation()
    {
        return Quaternion.Euler(new Vector3(0.0f, camera.transform.eulerAngles.y, 0.0f));
    }

    private void MovePlayerByVector(Vector3 movementVector)
    {
        GetComponent<CharacterController>().Move(movementVector);
    }

    private void HandleInvalidPosition()
    {
        if (transform.position.y != originalPlayerPosition.y)
            transform.position = lastValidPlayerPosition;
        else
            lastValidPlayerPosition = transform.position;
    }
}
