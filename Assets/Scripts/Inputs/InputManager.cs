using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimationHandler animationHandler;
    PlayerMovement playerMovement; 

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY; 

    public float moveAmount; 
    public float verticalInput;
    public float horizontalInput;

    public bool s_input; 

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
        playerMovement = GetComponent<PlayerMovement>(); 
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            //Movement Inputs
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            //Action Inputs
            playerControls.PlayerActions.Shift.performed += i => s_input = true;
            playerControls.PlayerActions.Shift.canceled += i => s_input = false; 
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
        HandleSprintingInput();
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

    private void HandleSprintingInput()
    {
        if(s_input && moveAmount > 0.5f)
        {
            playerMovement.isSprinting = true; 
        } 
        else
        {
            playerMovement.isSprinting = false; 
        }
    }
}
