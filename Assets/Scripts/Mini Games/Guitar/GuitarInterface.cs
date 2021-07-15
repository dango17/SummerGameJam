using UnityEngine;

public class GuitarInterface : MonoBehaviour {
	public Score Score { get; private set; } = null;

	[Tooltip("Total number of button presses for the player to execute throughout the mini-game.")]
	[SerializeField]
	private int instructionsToSpawn = 10;
	private float spawnTimer = 0f;
	private float spawnTime = 0.75f;

	[SerializeField]
	private Vector3 instructionOffset = Vector2.zero;

	private InputManager inputManager = null;
	[SerializeField]
	private GameObject instruction = null;
	[SerializeField]
	private GameObject[] inputIcons = new GameObject[0];

	private void Start() {
		Score = GetComponentInChildren<Score>();
		Score.ShowScore();
		inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
		inputManager.SwitchInputMode(InputManager.InputModes.MiniGame);
	}

	private void Update() {
		spawnTimer -= Time.deltaTime;

		if (instructionsToSpawn > 0 && spawnTimer <= 0f) {
			SpawnInstruction();
		}
	}

	private void SpawnInstruction() {
		int index = Random.Range(0, inputIcons.Length);
		Instruction instructionInstance = Instantiate(instruction,
			inputIcons[index].transform.position + instructionOffset,
			Quaternion.identity,
			inputIcons[index].transform).GetComponent<Instruction>();
		instructionInstance.SetType(inputIcons[index].GetComponent<CustomButton>());
		spawnTimer = spawnTime;
		--instructionsToSpawn;
	}
}
