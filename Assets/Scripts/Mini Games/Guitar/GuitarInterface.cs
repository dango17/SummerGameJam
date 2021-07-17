using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns a sequence of inputs for the player to enter.
/// </summary>
public class GuitarInterface : MonoBehaviour {
	/// <summary>
	/// Score achieved by the player through playing the mini-game.
	/// </summary>
	public Score Score { get; private set; } = null;
	/// <summary>
	/// The guitar which spawned this interface.
	/// </summary>
	public Guitar GuitarOwner { get { return guitar; } set { guitar = value; } }
	public LinkedList<Instruction> SpawnedInstructions { get; set; } = new LinkedList<Instruction>();

	[Tooltip("Total number of button presses for the player to execute throughout the mini-game.")]
	[SerializeField]
	private int instructionsToSpawn = 10;
	private float spawnTimer = 0f;
	[Tooltip("Amount of time to pass by before the next instruction is spawned.")]
	[SerializeField]
	private float spawnTime = 0.65f;

	[Tooltip("Instruction spawn position offset from the input icons")]
	[SerializeField]
	private Vector3 instructionOffset = Vector2.zero;

	private Guitar guitar = null;

	[SerializeField]
	private GameObject instructionPrefab = null;
	[SerializeField]
	private GameObject[] buttonPrompts = new GameObject[0];

	private void Start() {
		Score = GetComponentInChildren<Score>();
		Score.ShowScore();

		foreach (GameObject buttonPrompt in buttonPrompts) {
			buttonPrompt.GetComponent<ButtonPrompt>().LinkButton(HandleInput);
		}
	}

	private void Update() {
		spawnTimer -= Time.deltaTime;

		if (instructionsToSpawn > 0 && spawnTimer <= 0f) {
			SpawnInstruction();
		}
	}

	private void OnDestroy() {
		guitar.CompleteEvent();
	}

	private void SpawnInstruction() {
		int index = Random.Range(0, buttonPrompts.Length);
		Instruction instructionInstance = Instantiate(instructionPrefab,
			buttonPrompts[index].transform.position + instructionOffset,
			Quaternion.identity,
			buttonPrompts[index].transform).GetComponent<Instruction>();
		instructionInstance.SetType(buttonPrompts[index].GetComponent<ButtonPrompt>());
		spawnTimer = spawnTime;
		--instructionsToSpawn;
	}

	private void HandleInput(ButtonPrompt buttonPrompt) {
		if (SpawnedInstructions.Count > 0) {
			// Always execute the first instruction before any others.
			if (SpawnedInstructions.First.Value.CorresspondingInput == buttonPrompt.InputButton.ToString()) {
				Destroy(SpawnedInstructions.First.Value.gameObject);
			}
		}
	}
}
