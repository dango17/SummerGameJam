using UnityEngine;

public class Food : Interactable {
	/// <summary>
	/// Powers the player's ability by increasing it's radius by energy amount.
	/// </summary>
	[Tooltip("Amount of energy the player receives upon consuming the food.")]
	[SerializeField]
	private float energy = 0.2f;

	public override void Use() {
		if (timesUsed < usesAvailable) {
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbility>().IncreasePower(energy);
			isUsed = true;
			++timesUsed;
		}
		
		if (timesUsed >= usesAvailable) {
			Destroy(gameObject);
			Deselect();
		}
	}
}
