using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimationHandler animationHandler; 

    public Vector2 movementInput;

    private float moveAmount; 
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
        HandleInteractionInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)); 
        animationHandler.UpdateAnimatorValues(0, moveAmount); 
    }

    private void HandleInteractionInput() {
        // Test interact button is pressed and player has selected an interactable item.
        if (playerControls.PlayerInteraction.Interact.triggered && Interactable.CurrentSelection) {
            Interactable.CurrentSelection.Use();
		}
	}
}
