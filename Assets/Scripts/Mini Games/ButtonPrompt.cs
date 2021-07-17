using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonPrompt : MonoBehaviour {
	public enum ButtonTypes {
		Press,
		MultiplePress,
		Hold,
		Count
	}

	public int TimeToActivate { get { return TimeToActivate; } set { timeToActivate = value; } }
	public bool DestroyOnUse { get { return destroyOnUse; } set { destroyOnUse = value; } }
	public ButtonTypes ButtonType { get { return buttonType; } set { buttonType = value; } }
	public InputAction InputButton { get { return inputButton; } private set { } }

	[Tooltip("Number of presses to activate button or length of time to hold down depending on button type.")]
	[SerializeField]
	private int timeToActivate = 1;
	private int timesActivated = 0;
	private float duration = 0;

	private bool pressed = false;
	private bool held = false;
	[SerializeField]
	private bool destroyOnUse = false;

	private ButtonTypes buttonType = ButtonTypes.Press;

	private MethodToExecute methodToExecute = null;
	[SerializeField]
	private InputAction inputButton = null;

	public delegate void MethodToExecute();

	public void Use() {
		methodToExecute();

		if (destroyOnUse) {
			Destroy(gameObject);
		}
	}

	public void LinkButton(MethodToExecute methodToLink) {
		methodToExecute = methodToLink;
	}

	private void Start() {
		inputButton.Enable();
	}

	// Update is called once per frame
	private void Update() {
		inputButton.performed += i => pressed = true;
		inputButton.started += i => held = true;

		if (buttonType == ButtonTypes.Hold) {
			if (pressed) {
				duration += Time.deltaTime;
			}

			if (duration >= timeToActivate) {
				Use();
			}
		} else {
			if (held) {
				++timesActivated;
			}

			if (timesActivated >= timeToActivate) {
				Use();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Instruction") && collision.GetComponent<Instruction>().CorresspondingInput == inputButton.ToString()) {
			instructions.AddLast(collision.GetComponent<Instruction>());
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("Instruction") && collision.GetComponent<Instruction>().CorresspondingInput == inputButton.ToString()) {
			if (instructions.Contains(collision.GetComponent<Instruction>())) {
				instructions.Remove(collision.GetComponent<Instruction>());
			}
		}
	}
}
