using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract class to be implemented by all interactable items.
/// </summary>
public abstract class Interactable : MonoBehaviour {
	/// <summary>
	/// True if the item has been activated at least once, otherwise false.
	/// </summary>
	public bool IsUsed { get { return isUsed; } set { } }
	/// <summary>
	/// Interactable currently selected by the player.
	/// </summary>
	public static Interactable CurrentSelection { get; set; } = null;

	[Tooltip("Amount of times the item can be activated.")]
	[SerializeField]
	protected int usesAvailable = 2;
	/// <summary>
	/// Amount of times the item has been activated.
	/// </summary>
	protected int timesUsed = 0;
	protected bool isUsed = false;
	/// <summary>
	/// UI text for displaying the name of the currently select item.
	/// </summary>
	protected Text interactPrompt = null;

	public abstract void Use();

	protected void Deselect() {
		CurrentSelection = null;
		interactPrompt.text = "";
	}

	/// <summary>
	/// Updates which item is currently selected by the player to the nearest item.
	/// </summary>
	/// <param name="other"> Possible player game object. </param>
	private void OnTriggerStay(Collider other) {
		if (other.CompareTag("Player")) {
			if (CurrentSelection && CurrentSelection != this) {
				if (Vector3.Distance(transform.position, other.transform.position) < Vector3.Distance(CurrentSelection.transform.position, other.transform.position)) {
					CurrentSelection = this;
					interactPrompt.text = gameObject.name;
				}
			} else {
				CurrentSelection = this;
				interactPrompt.text = gameObject.name;
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player")) {
			if (CurrentSelection = this) {
				CurrentSelection = null;
				interactPrompt.text = "";
			}
		}
	}
}
