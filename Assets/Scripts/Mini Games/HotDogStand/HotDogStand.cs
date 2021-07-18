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

	public override void Use() {
		inputManager.SwitchInputMode(InputManager.InputModes.MiniGame);
		GameObject ui = GameObject.FindGameObjectWithTag("UI");
		standInterfaceInstance = Instantiate(interfacePrefab,
			ui.transform.position,
			Quaternion.identity,
			ui.transform).gameObject;
		MiniGameScore = standInterfaceInstance.GetComponentInChildren<Score>();
		MiniGameScore.ShowScore();
		SpawnInstruction();
	}

	public override void CompleteEvent() {
		if (standInterfaceInstance) {
			Destroy(standInterfaceInstance);
		}

		TotalGameScore.AddScore(MiniGameScore.Scored);
		IsMiniGameActive = false;
		inputManager.SwitchInputMode(InputManager.InputModes.Player);
	}

	private void Start() {
		interactPrompt = GameObject.FindGameObjectWithTag("InteractPrompt").GetComponent<Text>();
		playerAbility = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbility>();
	}

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
		lastSpawnedButton.TimeToActivate = 4;
		lastSpawnedButton.DestroyOnUse = true;
		lastSpawnedButton.ButtonType = ButtonPrompt.ButtonTypes.MultiplePress;
		lastSpawnedButton.LinkButton(EatHotDog);
		++buttonsSpawned;
	}

	private void EatHotDog(ButtonPrompt buttonPrompt) {
		eatEffect.Play();
		const int hotDogScoreWorth = 5;
		MiniGameScore.AddScore(hotDogScoreWorth);
		const float hotDogEnergy = 0.2f;
		playerAbility.ChangePowerLevel(hotDogEnergy);
		SpawnInstruction();
	}
}
