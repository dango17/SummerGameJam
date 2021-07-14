using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimationHandler animationHandler; 

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY; 

    public float moveAmount; 
    public float verticalInput;
    public float horizontalInput;

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>(); 
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            //Move thorugh variables in input system 
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>(); 
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable(); 
    }

    public void HandleAllInputs()
    {
        HandleMovementInput(); 
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)); 
        animationHandler.UpdateAnimatorValues(0, moveAmount);

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x; 
    }
}
