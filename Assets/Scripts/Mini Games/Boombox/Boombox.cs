using UnityEngine;
using UnityEngine.UI;

public class Boombox : MiniGame {
	private void Start() {
		interactPrompt = GameObject.FindGameObjectWithTag("InteractPrompt").GetComponent<Text>();
	}

	public override void Use() {
		Debug.Log("Using Boombox");
		inputManager.SwitchInputMode(InputManager.InputModes.MiniGame);
		// TODO: start dancing (play animation)
	}

	public override void CompleteEvent() {
		Debug.Log("Finished Using Boombox");
		// TODO: attract AI.
		IsMiniGameActive = false;
		inputManager.SwitchInputMode(InputManager.InputModes.Player);
	}
}
