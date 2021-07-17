using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boombox : MiniGame {
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
