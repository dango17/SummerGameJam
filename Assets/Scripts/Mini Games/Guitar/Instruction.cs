using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Acts as a prompt for which type of input the player should enter.
/// </summary>
public class Instruction : MonoBehaviour {
	/// <summary>
	/// The input to enter.
	/// </summary>
	public string CorresspondingInput { get; private set; } = "";

	private int movementSpeed = 320;
	[Tooltip("The amount of score this is worth.")]
	[SerializeField]
	private int scoreValue = 5;
	private GuitarInterface guitarInterface = null;

	/// <summary>
	/// Sets the type of input this instruction represents.
	/// </summary>
	/// <param name="button"> Button associated with this input type. </param>
	public void SetType(ButtonPrompt button) {
		GetComponent<Image>().sprite = button.GetComponent<Image>().sprite;
		CorresspondingInput = button.InputButton.ToString();
	}

	private void Start() {
		guitarInterface = GameObject.FindGameObjectWithTag("GuitarUI").GetComponent<GuitarInterface>();
	}

	private void Update() {
		transform.position -= new Vector3(0, movementSpeed * Time.deltaTime, 0);
	}

	private void OnDestroy() {
		if (!guitarInterface) {
			return;
		}

		guitarInterface.Score.AddScore(scoreValue);

		if (FindObjectsOfType<Instruction>().Length == 0) {
			// Mini-game should close once all instructions have been destroyed.
			Destroy(guitarInterface.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("DestructionZone")) {
			guitarInterface.Score.SubtractScore(scoreValue);
			Destroy(gameObject);
		}

		if (collision.CompareTag("ButtonPrompt") && CorresspondingInput == collision.GetComponent<ButtonPrompt>().InputButton.ToString()) {
			guitarInterface.SpawnedInstructions.AddLast(this);
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("ButtonPrompt") && CorresspondingInput == collision.GetComponent<ButtonPrompt>().InputButton.ToString()) {
			if (guitarInterface.SpawnedInstructions.Contains(this)) {
				guitarInterface.SpawnedInstructions.Remove(this);
			}
		}
	}
}
