using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;

    public float defaultPosition;     //Camera always snaps back here
    public Transform cameraTransform; //The transform of camera obj in scene
    public Transform targetTransform; //Follow Camera 
    public Transform CameraPivot;     //CameraPivot
    public LayerMask collisionLayers; //Collide with set game layers
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition; 

    [Header("Camera-Speed")]
    public float cameraFollowSpeed = 32f;
    public float cameraLookSpeed = 32;
    public float cameraPivotSpeed = 32;

    [Header("Camera-Collisions")]
    public float cameraCollisionOffSet = 0.2f;
    public float minimumCollisionOffSet = 0.2f; 
    public float cameraCollisionRadius = 2; 

    [Header("Camera-Clamp")]
    public float minimumPivotAngle = -35;
    public float maximumPivotAngle = 35; 


    [Header("Axis Angles")]
    public float lookAngle; //Up - Down
    public float pivotAngle; //Left - Right

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>(); 
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        defaultPosition = cameraTransform.localPosition.z;
        cameraTransform = Camera.main.transform; 
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
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
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle); 

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        CameraPivot.localRotation = targetRotation;    
    } 

    public void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - CameraPivot.position;
        direction.Normalize(); 

        if(Physics.SphereCast 
            (CameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(CameraPivot.position, hit.point);
            targetPosition =- (distance - cameraCollisionOffSet); 
        } 

        if(Mathf.Abs(targetPosition) < minimumCollisionOffSet)
        {
            targetPosition = targetPosition - minimumCollisionOffSet;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition; 
    }
}
