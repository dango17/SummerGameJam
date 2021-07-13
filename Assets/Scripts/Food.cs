using UnityEngine;

public class Food : Interactable {
	/// <summary>
	/// Used to power the player's ability.
	/// </summary>
	[Tooltip("Amount of energy the player receives upon consuming the food.")]
	[SerializeField]
	private int energy = 0;

	public override void Use() {
		if (timesUsed < usesAvailable) {
			Debug.Log("Used Food: " + gameObject.name);
			isUsed = true;
			++timesUsed;
		}
		
		if (timesUsed >= usesAvailable) {
			Destroy(gameObject);
		}
	}
}
