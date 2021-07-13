using UnityEngine;

/// <summary>
/// Abstract class to be implemented by all interactable items.
/// Not to be instantiated by itself.
/// </summary>
public abstract class Interactable : MonoBehaviour {
	/// <summary>
	/// Interactable currently selected by the player.
	/// </summary>
	public static Interactable CurrentSelection { get; set; } = null;

	public abstract void Use();

	/// <summary>
	/// Updates which item is currently selected by the player to the nearest item.
	/// </summary>
	/// <param name="other"> Possible player game object. </param>
	private void OnTriggerStay(Collider other) {
		if (other.CompareTag("Player")) {
			if (CurrentSelection && CurrentSelection != this) {
				if (Vector3.Distance(transform.position, other.transform.position) < Vector3.Distance(CurrentSelection.transform.position, other.transform.position)) {
					CurrentSelection = this;
				}
			} else {
				CurrentSelection = this;
			}
		}
	}
}
