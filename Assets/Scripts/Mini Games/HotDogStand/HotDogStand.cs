using UnityEngine;

public class HotDogStand : MiniGame {
	/// <summary>
	/// Score achieved by the player through playing the mini-game.
	/// </summary>
	public Score Score { get; private set; } = null;

	[Tooltip("Total number of buttons for the player to mash to complete the mini-game.")]
	[SerializeField]
	private int buttonsToSpawn = 10;
	private int buttonsSpawned = 0;

	private Canvas standInterfacePrefab = null;
	private GameObject standInterfaceInstance = null;
	[SerializeField]
	private GameObject buttonPrefab = null;

	public override void Use() {
		GameObject ui = GameObject.FindGameObjectWithTag("UI");
		standInterfaceInstance = Instantiate(standInterfacePrefab,
			ui.transform.position,
			Quaternion.identity,
			ui.transform).gameObject;
		Score = standInterfaceInstance.GetComponent<Score>();
		Score.ShowScore();
		SpawnInstruction();
	}

	public override void CompleteEvent() {
		Score.AddScore(Score.Scored);
		IsMiniGameActive = false;
		inputManager.SwitchInputMode(InputManager.InputModes.Player);
	}

	private void SpawnInstruction() {
		if (buttonsSpawned >= buttonsToSpawn) {
			CompleteEvent();
			return;
		}

		ButtonPrompt lastSpawnedButton = null;
		Vector2 spawnPosition = Vector2.zero;
		lastSpawnedButton = Instantiate(buttonPrefab,
			spawnPosition,
			Quaternion.identity,
			standInterfacePrefab.transform).GetComponent<ButtonPrompt>();
		lastSpawnedButton.TimeToActivate = 4;
		lastSpawnedButton.DestroyOnUse = true;
		lastSpawnedButton.ButtonType = ButtonPrompt.ButtonTypes.MultiplePress;
		// TODO: link to spawning another button.
		lastSpawnedButton.LinkButton(SpawnInstruction);
		++buttonsSpawned;
	}
}
