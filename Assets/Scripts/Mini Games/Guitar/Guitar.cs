using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used for starting/completing the guitar mini-game.
/// </summary>
public class Guitar : MiniGame {
	[Tooltip("The player's total game score.")]
	[SerializeField]
	private Score score = null;
	private GuitarInterface guitarInterface = null;

	private void Start() {
		interactPrompt = GameObject.FindGameObjectWithTag("InteractPrompt").GetComponent<Text>();
		inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
		
	}

	public override void Use() {
		if (timesUsed < usesAvailable) {
			GameObject canvas = GameObject.FindGameObjectWithTag("UI");
			GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("Idle");
			GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = true;
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
		GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("Movement");
		inputManager.SwitchInputMode(InputManager.InputModes.Player);
	}
}
