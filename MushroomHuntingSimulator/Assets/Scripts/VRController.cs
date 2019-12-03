using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 0.1f;
    [SerializeField]
    private float maxSpeed = 1.0f;
    [SerializeField]
    private SteamVR_Action_Boolean movePress;
    [SerializeField]
    private SteamVR_Action_Vector2 moveValue;

    private CharacterController characterController;
    private Transform cameraRig;
    private Transform head;
    private float speed = 0.0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        cameraRig = SteamVR_Render.Top().origin;
        head = SteamVR_Render.Top().head;
    }

    private void Update()
    {
        HandleHead();
        HandleHeight();
        HandleMovement();
    }

    private void HandleHead()
    {
        // Head movements are transforming VRController and its child CameraRig should not transform on its own, that's why CameraRig transform is being reset
        Vector3 oldPosition = cameraRig.position;
        Quaternion oldRotation = cameraRig.rotation;
        transform.eulerAngles = new Vector3(0.0f, head.rotation.eulerAngles.y, 0.0f);
        cameraRig.position = oldPosition;
        cameraRig.rotation = oldRotation;
    }

    private void HandleHeight()
    {
        ResizeCharacterControllerHeight();
        RepositionCharacterControllerCenter();
    }

    // Resize height of character controller based on level of headset being off the ground in terms of local space of CameraRig
    private void ResizeCharacterControllerHeight()
    {
        float headHeight = Mathf.Clamp(head.localPosition.y, 1, 2);
        characterController.height = headHeight;
    }

    // After resizing character controller's height it's necessary to reposition its center
    private void RepositionCharacterControllerCenter()
    {
        Vector3 newCenter = Vector3.zero;
        newCenter.y = characterController.height / 2;
        newCenter.y += characterController.skinWidth;
        newCenter.x = head.localPosition.x;
        newCenter.z = head.localPosition.z;
        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;
        characterController.center = newCenter;
    }

    private void HandleMovement()
    {
        if (NotMoving())
            ResetSpeed();
        ApplyMovement(CalculateMovement());
    }

    private bool NotMoving()
    {
        return moveValue.axis.magnitude == 0;
    }

    private void ResetSpeed()
    {
        speed = 0;
    }

    private bool ButtonPressed()
    {
        return movePress.state;
    }

    private void ApplyMovement(Vector3 movement)
    {
        characterController.Move(movement);
    }

    private Vector3 CalculateMovement()
    {
        Quaternion orientation = CalculateOrientation();
        speed += moveValue.axis.magnitude * sensitivity;
        speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

        return Vector3.zero + orientation * (speed * Vector3.forward) * Time.deltaTime;
    }

    private Quaternion CalculateOrientation()
    {
        float rotation = Mathf.Atan2(moveValue.axis.x, moveValue.axis.y);
        rotation *= Mathf.Rad2Deg;

        return Quaternion.Euler(new Vector3(0.0f, transform.eulerAngles.y + rotation, 0.0f));
    }
}
