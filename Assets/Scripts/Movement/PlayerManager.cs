using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    CameraManager cameraManager; 
    PlayerMovement playerMovement;
    Animator animator;

    public bool isInteracting; 

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>(); 
        playerMovement = GetComponent<PlayerMovement>(); 
    } 

    private void Update()
    {
        inputManager.HandleAllInputs(); 
    }

    private void FixedUpdate()
    {
        playerMovement.HandleAllMovement(); 
    } 

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
        isInteracting = animator.GetBool("isInteracting");
    }
}
