using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used for starting/completing the guitar mini-game.
/// </summary>
public class Guitar : MiniGame {
	[SerializeField]
	private Score score = null;
	private GuitarInterface guitarInterface = null;

	private void Start() {
		interactPrompt = GameObject.FindGameObjectWithTag("InteractPrompt").GetComponent<Text>();
	}

	public override void Use() {
		if (timesUsed < usesAvailable) {
			GameObject canvas = GameObject.FindGameObjectWithTag("UI");
			guitarInterface = Instantiate(interfacePrefab, canvas.transform.position, Quaternion.identity, canvas.transform).GetComponent<GuitarInterface>();
			guitarInterface.GuitarOwner = this;
			IsMiniGameActive = true;
			inputManager.SwitchInputMode(InputManager.InputModes.MiniGame);
			isUsed = true;
			++timesUsed;
		}
	}

	public override void CompleteEvent() {
		if (guitarInterface) {
			score.AddScore(guitarInterface.Score.Scored);
			Destroy(guitarInterface.gameObject);
		}

		IsMiniGameActive = false;
		inputManager.SwitchInputMode(InputManager.InputModes.Player);
	}
}
