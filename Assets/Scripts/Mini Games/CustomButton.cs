using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomButton : MonoBehaviour {
	public InputAction InputButton { get { return inputButton; } private set { } }

	private LinkedList<Instruction> instructions = new LinkedList<Instruction>();

	[SerializeField]
	private InputAction inputButton = null;

	private void Start() {
		inputButton.Enable();
	}

	// Update is called once per frame
	private void Update() {
		HandleInput();
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

	private void HandleInput() {
		if (inputButton.triggered && instructions.Count > 0) {
			if (instructions.First.Value.CorresspondingInput == inputButton.ToString()) {
				instructions.First.Value.Destroy();
			}
		}
	}
}
