using UnityEngine;
using UnityEngine.UI;

public class Boombox : MiniGame {
	private void Start() {
		interactPrompt = GameObject.FindGameObjectWithTag("InteractPrompt").GetComponent<Text>();
		inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
	}

	public override void Use() {
		if (timesUsed >= usesAvailable) {
			return;
		}

		inputManager.SwitchInputMode(InputManager.InputModes.MiniGame);
		IsMiniGameActive = true;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsDancing", true);
		++timesUsed;
	}

	public override void CompleteEvent() {
		inputManager.SwitchInputMode(InputManager.InputModes.Player);
		IsMiniGameActive = false;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsDancing", false);
	}
}
