using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	public PlayerControls PlayerControls { get { return playerControls; } set { } }

	public enum InputModes {
		Player,
		MiniGame,
		Count
	}

	public Vector2 movementInput;
	public Vector2 cameraInput;

	public float cameraInputX;
	public float cameraInputY;

	public float moveAmount;
	public float verticalInput;
	public float horizontalInput;

	public bool s_input;

	private InputModes inputMode = InputModes.Player;

	private PlayerControls playerControls;
	private AnimationHandler animationHandler;
	private PlayerMovement playerMovement;
	private PlayerAbility playerAbility;
	[HideInInspector] PickUp pickUpA;

	public void HandleAllInputs() {
		switch (inputMode) {
			case InputModes.Player: {
				HandleMovementInput();
				HandleInteractionInput();
				HandleSprintingInput();
				HandleActionsInput();
				HandlePickup();
				break;
			}
			case InputModes.MiniGame: {
				// Leave empty. Each mini-game should have their own responses to input.
				break;
			}
			default: {
				break;
			}
		}
	}

	/// <summary>
	/// Switches the input mode to a different type.
	/// </summary>
	/// <param name="newMode"> Mode to switch to.</param>
	public void SwitchInputMode(InputModes newMode) {
		inputMode = newMode;
	}

	private void Awake() {
		animationHandler = GetComponent<AnimationHandler>();
		playerMovement = GetComponent<PlayerMovement>();
		playerAbility = GetComponent<PlayerAbility>();
		pickUpA = FindObjectOfType<PickUp>();
	}

	private void OnEnable() {
		if (playerControls == null) {
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

	private void OnDisable() {
		playerControls.Disable();
	}

	private void HandleMovementInput() {
		verticalInput = movementInput.y;
		horizontalInput = movementInput.x;

		moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
		animationHandler.UpdateAnimatorValues(0, moveAmount);

		cameraInputY = cameraInput.y;
		cameraInputX = cameraInput.x;
	}

	private void HandleSprintingInput() {
		if (s_input && moveAmount > 0.5f) {
			playerMovement.isSprinting = true;
		} else {
			playerMovement.isSprinting = false;
		}
	}

	private void HandleInteractionInput() {
		// Test interact button is pressed and player has selected an interactable item.
		if (playerControls.PlayerInteraction.Interact.triggered && Interactable.CurrentSelection) {
			Interactable.CurrentSelection.Use();
		}
	}

	private void HandleActionsInput() {
		if (playerControls.PlayerActions.Ability.triggered) {
			playerAbility.Use();
		}
	}

	private void HandlePickup()
    {
        if (playerControls.PlayerPickup.PickUp.triggered && pickUpA.canPickup == true)
        {
            pickUpA.PickUpObject();
        }

        if (playerControls.PlayerPickup.Drop.triggered)
        {
            pickUpA.DropObject();
        }
    }
}
