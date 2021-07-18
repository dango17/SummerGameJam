using UnityEngine;
using UnityEngine.UI;

public class Food : Interactable {
	/// <summary>
	/// Powers the player's ability by increasing it's radius by energy amount.
	/// </summary>
	[Tooltip("Amount of energy the player receives upon consuming the food.")]
	[SerializeField]
	private float energy = 0.2f;

	public override void Use() {
		if (timesUsed < usesAvailable) {
			isUsed = true;
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbility>().ChangePowerLevel(energy);
			++timesUsed;
		}
		
		if (timesUsed >= usesAvailable) {
			Destroy(gameObject);
			Deselect();
		}
	}

	private void Start() {
		interactPrompt = GameObject.FindGameObjectWithTag("InteractPrompt").GetComponent<Text>();
	}
}
