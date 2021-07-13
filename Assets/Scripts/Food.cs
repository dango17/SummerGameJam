using UnityEngine;

public class Food : Interactable {
	/// <summary>
	/// Used to power the player's ability.
	/// </summary>
	[SerializeField]
	private int energy = 0;

	public override void Use() {
		Debug.Log("Used Food: " + gameObject.name);
	}
}
