using UnityEngine;

/// <summary>
/// Used for starting/completing the guitar mini-game.
/// </summary>
public class Guitar : MiniGame {
	[SerializeField]
	private Score score = null;
	private GuitarInterface guitarInterface = null;

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
