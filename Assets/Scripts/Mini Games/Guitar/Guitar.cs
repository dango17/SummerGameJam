using UnityEngine;

/// <summary>
/// Used for starting/completing the guitar mini-game.
/// </summary>
public class Guitar : Interactable {
	/// <summary>
	/// Is the player engaging with the mini-game.
	/// </summary>
	public bool IsMiniGameActive { get; private set; } = false;

	[SerializeField]
	private InputManager inputManager = null;
	[SerializeField]
	private Score score = null;
	private GuitarInterface guitarInterface = null;
	[SerializeField]
	private GameObject guitarInterfacePrefab = null;

	public override void Use() {
		if (timesUsed < usesAvailable) {
			GameObject canvas = GameObject.FindGameObjectWithTag("UI");
			guitarInterface = Instantiate(guitarInterfacePrefab, canvas.transform.position, Quaternion.identity, canvas.transform).GetComponent<GuitarInterface>();
			guitarInterface.GuitarOwner = this;
			IsMiniGameActive = true;
			inputManager.SwitchInputMode(InputManager.InputModes.MiniGame);
			isUsed = true;
			++timesUsed;
		}
	}

	public void CompleteEvent() {
		if (guitarInterface) {
			score.AddScore(guitarInterface.Score.Scored);
			Destroy(guitarInterface.gameObject);
		}

		inputManager.SwitchInputMode(InputManager.InputModes.Player);
	}
}
