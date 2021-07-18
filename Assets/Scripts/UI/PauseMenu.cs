using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
	public bool Paused { get; private set; } = false;

	public void Pause(bool pause) {
		Paused = pause;
		gameObject.SetActive(Paused);
	}
}
