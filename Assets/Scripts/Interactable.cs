using UnityEngine;

/// <summary>
/// Abstract class to be implemented by all interactable items.
/// </summary>
public abstract class Interactable : MonoBehaviour {
	/// <summary>
	/// Interactable currently selected by the player.
	/// </summary>
	public static Interactable CurrentSelection { get; set; } = null;

	/// <summary>
	/// True if the item has been activated at least once, otherwise false.
	/// </summary>
	public bool IsUsed { get { return isUsed; } set { } }

	[Tooltip("Amount of times the item can be activated.")]
	[SerializeField]
	protected int usesAvailable = 1;
	/// <summary>
	/// Amount of times the item has been activated.
	/// </summary>
	protected int timesUsed = 0;
	protected bool isUsed = false;

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

	private void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player")) {
			if (CurrentSelection = this) {
				CurrentSelection = null;
			}
		}
	}
}
