using UnityEngine;

public class Guitar : Interactable {
	[SerializeField]
	private GameObject guitarInterfacePrefab = null;

	public override void Use() {
		if (timesUsed < usesAvailable) {
			GameObject canvas = GameObject.FindGameObjectWithTag("UI");
			Instantiate(guitarInterfacePrefab, canvas.transform.position, Quaternion.identity, canvas.transform);
			isUsed = true;
			++timesUsed;
		}
	}
}
