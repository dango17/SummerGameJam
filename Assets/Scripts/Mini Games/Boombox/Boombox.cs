using UnityEngine;
using UnityEngine.UI;

public class Boombox : MiniGame {
	private void Start() {
		interactPrompt = GameObject.FindGameObjectWithTag("InteractPrompt").GetComponent<Text>();
	}

	public override void Use() {
		Debug.Log("Using Boombox");
		inputManager.SwitchInputMode(InputManager.InputModes.MiniGame);
		IsMiniGameActive = true;
	}

	public override void CompleteEvent() {
		IsMiniGameActive = false;
		inputManager.SwitchInputMode(InputManager.InputModes.Player);
	}
}
