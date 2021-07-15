using UnityEngine;

public class GuitarInterface : MonoBehaviour {
	private int instructionsToSpawn = 10;
	private float spawnTimer = 0f;
	private float spawnTime = 0.75f;

	[SerializeField]
	private GameObject[] spawnPositions = new GameObject[0];

	[SerializeField]
	private GameObject instruction = null;
	[SerializeField]
	private Sprite[] instructionIcons = new Sprite[0];

	private void Update() {
		spawnTimer -= Time.deltaTime;

		if (instructionsToSpawn > 0 && spawnTimer <= 0f) {
			SpawnInstruction();
		}
	}

	private void SpawnInstruction() {
		int index = Random.Range(0, spawnPositions.Length);
		Instruction instructionInstance = Instantiate(instruction, spawnPositions[index].transform.position, Quaternion.identity, spawnPositions[index].transform).GetComponent<Instruction>();
		instructionInstance.SetType(instructionIcons[index]);
		spawnTimer = spawnTime;
		--instructionsToSpawn;
	}
}
