using UnityEngine;
using UnityEngine.UI;

public class HotDogStand : MiniGame {
	/// <summary>
	/// Score achieved by the player through playing the mini-game.
	/// </summary>
	public Score MiniGameScore { get; private set; } = null;
	
	[Tooltip("Total number of buttons for the player to mash to complete the mini-game.")]
	[SerializeField]
	private int buttonsToSpawn = 10;
	private int buttonsSpawned = 0;

	private PlayerAbility playerAbility = null;
	[Tooltip("The player's total game score.")]
	[SerializeField]
	private Score TotalGameScore = null;
	[SerializeField]
	private ParticleSystem eatEffect = null;
	private GameObject standInterfaceInstance = null;
	[SerializeField]
	private GameObject[] buttonPromptPrefabs = null;

	/// <summary>
	/// Starts the mini-game event.
	/// </summary>
	public override void Use() {
		inputManager.SwitchInputMode(InputManager.InputModes.MiniGame);
		GameObject ui = GameObject.FindGameObjectWithTag("UI");
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("Idle");
		GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = true;
		standInterfaceInstance = Instantiate(interfacePrefab,
			ui.transform.position,
			Quaternion.identity,
			ui.transform).gameObject;
		MiniGameScore = standInterfaceInstance.GetComponentInChildren<Score>();
		MiniGameScore.ShowScore();
		SpawnInstruction();
	}

	/// <summary>
	/// Ends the mini-game event.
	/// </summary>
	public override void CompleteEvent() {
		if (standInterfaceInstance) {
			Destroy(standInterfaceInstance);
		}

		TotalGameScore.AddScore(MiniGameScore.Scored);
		IsMiniGameActive = false;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("Movement");
		inputManager.SwitchInputMode(InputManager.InputModes.Player);
	}

	private void Start() {
		interactPrompt = GameObject.FindGameObjectWithTag("InteractPrompt").GetComponent<Text>();
		playerAbility = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbility>();
		inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
	}

	/// <summary>
	/// Create's a UI prompt telling the player what input to enter.
	/// </summary>
	private void SpawnInstruction() {
		if (buttonsSpawned >= buttonsToSpawn) {
			CompleteEvent();
			return;
		}

		ButtonPrompt lastSpawnedButton = null;
		int buttonPromptIndex = Random.Range(0, buttonPromptPrefabs.Length);
		lastSpawnedButton = Instantiate(buttonPromptPrefabs[buttonPromptIndex],
			standInterfaceInstance.transform.position,
			Quaternion.identity,
			standInterfaceInstance.transform).GetComponent<ButtonPrompt>();
		const int pressesToUseButton = 4;
		lastSpawnedButton.TimeToActivate = pressesToUseButton;
		lastSpawnedButton.DestroyOnUse = true;
		lastSpawnedButton.ShowProgressBar = true;
		lastSpawnedButton.ButtonType = ButtonPrompt.ButtonTypes.MultiplePress;
		lastSpawnedButton.LinkMethodToButton(EatHotDog);
		++buttonsSpawned;
	}

	/// <summary>
	/// Increases the player's power level.
	/// </summary>
	/// <param name="buttonPrompt"> Button that triggered this method. </param>
	private void EatHotDog(ButtonPrompt buttonPrompt) {
		eatEffect.Play();
		const int hotDogScoreWorth = 5;
		MiniGameScore.AddScore(hotDogScoreWorth);
		const float hotDogEnergy = 0.2f;
		playerAbility.ChangePowerLevel(hotDogEnergy);
		SpawnInstruction();
	}
}
