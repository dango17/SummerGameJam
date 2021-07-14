﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager; 

    public Transform targetTransform; //Follow Camera 
    public Transform CameraPivot; 
    private Vector3 cameraFollowVelocity = Vector3.zero;

    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2;
    public float cameraPivotSpeed = 2; 

    public float lookAngle; //Up - Down
    public float pivotAngle; //Left - Right

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>(); 
        targetTransform = FindObjectOfType<PlayerManager>().transform;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera(); 
    }

    public void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp
            (transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition; 
    } 

    public void RotateCamera()
    {
        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        CameraPivot.localRotation = targetRotation;  
      
    }
}
