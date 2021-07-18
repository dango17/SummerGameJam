using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
	public bool Paused { get; private set; } = false;

	private MenuControls menuControls = null;
	private Canvas pauseMenuCanvas = null;

	public void Pause(bool pause) {
		Paused = pause;
		pauseMenuCanvas.enabled = Paused;

		if (Paused) {
			Time.timeScale = 0.0f;
		} else {
			const float regularTimeScale = 1.0f;
			Time.timeScale = regularTimeScale;
		}
	}

	private void Awake() {
		pauseMenuCanvas = GetComponent<Canvas>();
		menuControls = new MenuControls();
		menuControls.Enable();
	}

	private void Update() {
		// Listen for pause key being pressed.
		if (menuControls.PauseMenu.Pause.triggered) {
			Pause(!Paused);
		}
	}
}
